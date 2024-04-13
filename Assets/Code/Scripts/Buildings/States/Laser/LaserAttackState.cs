using System.Linq;
using Cysharp.Threading.Tasks;
using SustainTheStrain.Buildings.States;
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
            laser.Timer.Time -= Time.deltaTime;
            laser.Area.Update(laser.transform.position, laser.Config.Radius, laser.Config.Mask);
            
            if (laser.Area.Entities.Contains(_target) is false)
                return new LaserIdleState();
            
            laser.Orientation = _target.transform.position;
            if (!laser.Timer.IsTimeOver) return this;
            
            _target.Damage(laser.Config.Damage);
            laser.Timer.Time = laser.Config.Cooldown;

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