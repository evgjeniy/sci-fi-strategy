using NTC.Pool;
using SustainTheStrain.Units;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryAttackState : IUpdatableState<Artillery>
    {
        private readonly Area<Damageble> _explodeArea = new(conditions: damageable => !damageable.IsFlying && damageable.Team != Team.Player);
        private readonly Damageble _target;

        public ArtilleryAttackState(Damageble target) => _target = target;

        public IUpdatableState<Artillery> Update(Artillery artillery)
        {
            artillery.Area.Update(artillery.transform.position, artillery.Config.Radius, artillery.Config.Mask);

            if (_target.IsNotIn(artillery.Area))
                return new ArtilleryIdleState();

            artillery.Orientation = _target.transform.position;

            if (artillery.Timer.IsOver)
            {
                NightPool.Spawn(artillery.Config.ProjectilePrefab)
                    .With(x => x.transform.position = artillery.SpawnPointProvider.SpawnPoint.position)
                    .LaunchTo(_target, onComplete: damageable => Explosion(artillery, damageable));
                
                artillery.Timer.ResetTime(artillery.Config.Cooldown);
            }

            return this;
        }

        private void Explosion(Artillery artillery, Damageble target)
        {
            _explodeArea.Update(target.transform.position, artillery.Config.ExplosionRadius, artillery.Config.Mask);

            foreach (var damageable in _explodeArea.Entities)
                damageable.Damage(artillery.Config.Damage * artillery.EnergySystem.DamageMultiplier);

            if (artillery.Config.NextLevelConfig != null) return;
            if (artillery.AttackCounter % artillery.EnergySystem.Settings.PassiveSkill.AttackFrequency != 0) return;
            if (artillery.EnergySystem.CurrentEnergy != artillery.EnergySystem.MaxEnergy) return;

            foreach (var damageable in _explodeArea.Entities)
                artillery.EnergySystem.Settings.PassiveSkill.EnableSkill(damageable.gameObject);
        }
    }
}