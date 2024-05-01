using Cysharp.Threading.Tasks;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class LaserAttackState : IUpdatableState<Laser>
    {
        private const float LineVisualDuration = 0.5f;
        private readonly Damageble _target;

        public LaserAttackState(Damageble target) => _target = target;

        public IUpdatableState<Laser> Update(Laser laser)
        {
            laser.Area.Update(laser.transform.position, laser.Config.Radius, laser.Config.Mask);
            
            if (_target.IsNotIn(laser.Area))
                return new LaserIdleState();
            
            laser.Orientation = _target.transform.position;
            if (!laser.Timer.IsOver) return this;

            _target.DeepDamage(laser.Config.Damage * laser.EnergySystem.DamageMultiplier);

            if (laser.Config.NextLevelConfig == null)
                if (laser.AttackCounter % laser.EnergySystem.Settings.PassiveSkill.AttackFrequency == 0)
                    if (laser.EnergySystem.CurrentEnergy == laser.EnergySystem.MaxEnergy)
                        laser.EnergySystem.Settings.PassiveSkill.EnableSkill(_target.gameObject);

            laser.AttackCounter++;
            laser.Timer.ResetTime(laser.Config.Cooldown);

            EnableLineAttack(laser);
            
            return this;
        }

        private async void EnableLineAttack(Laser laser)
        {
            laser.Line.Enable();

            for (var time = 0.0f; time < LineVisualDuration; time += Time.deltaTime)
            {
                var isLineNull = laser.Line == null;

                if (_target == null)
                {
                    if (isLineNull) return;
                    break;
                }

                if (!isLineNull) 
                    laser.Line.SetPositions(new[] { laser.SpawnPointProvider.SpawnPoint.position, _target.transform.position });

                await UniTask.NextFrame();
            }

            laser.Line.Disable();
        }
    }
}