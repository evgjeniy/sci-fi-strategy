using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryAttackState : IUpdatableState<Artillery>
    {
        private readonly Area<Damageble> _explodeArea = new(conditions: damageable => damageable.Team != Team.Player);
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
                Object.Instantiate(artillery.Config.ProjectilePrefab, artillery.SpawnPointProvider.SpawnPoint)
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
            {
                damageable.Damage(artillery.Config.Damage);

                if (artillery.Config.IsMaxEnergy is false) continue;
                if (artillery.Config.HasPassiveSkill is false) continue;
                if (artillery.AttackCounter % artillery.Config.PassiveSkill.AttackFrequency != 0) continue;

                artillery.Config.PassiveSkill.EnableSkill(_target.gameObject);
            }
        }
    }
}