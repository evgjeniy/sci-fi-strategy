using System.Linq;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryAttackState : IArtilleryState
    {
        private readonly Area<Damageble> _explodeArea = new(conditions: damageable => damageable.Team != 1);
        private readonly Damageble _target;

        public ArtilleryAttackState(Damageble target) => _target = target;

        public IArtilleryState Update(Artillery artillery)
        {
            var artilleryData = artillery.Data;
            var artilleryConfig = artilleryData.Config.Value;

            artilleryData.Timer.Time -= Time.deltaTime;
            artilleryData.Area.Update(artillery.transform.position, artilleryConfig.Radius, artilleryConfig.Mask);

            if (artilleryData.Area.Entities.Contains(_target) is false)
                return new ArtilleryIdleState();

            artilleryData.Orientation.Value = _target.transform.position;

            if (artilleryData.Timer.IsTimeOver)
            {
                Object.Instantiate(artilleryConfig.ProjectilePrefab, artilleryData.ProjectileSpawnPoint)
                    .With(x => x.transform.position = artilleryData.ProjectileSpawnPoint.position)
                    .LaunchTo(_target, onComplete: damageable => Explosion(artilleryConfig, damageable));
                
                artilleryData.Timer.Time = artilleryConfig.Cooldown;
            }

            return this;
        }

        private void Explosion(ArtilleryBuildingConfig artilleryConfig, Damageble target)
        {
            _explodeArea.Update(target.transform.position, artilleryConfig.ExplosionRadius, artilleryConfig.Mask);

            foreach (var damageable in _explodeArea.Entities) 
                damageable.Damage(artilleryConfig.Damage);
        }
    }
}