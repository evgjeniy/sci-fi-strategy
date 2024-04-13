﻿using System.Linq;
using SustainTheStrain.Buildings.States;
using SustainTheStrain.Configs.Buildings;
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
            artillery.Timer.Time -= Time.deltaTime;
            artillery.Area.Update(artillery.transform.position, artillery.Config.Radius, artillery.Config.Mask);

            if (artillery.Area.Entities.Contains(_target) is false)
                return new ArtilleryIdleState();

            artillery.Orientation = _target.transform.position;

            if (artillery.Timer.IsTimeOver)
            {
                Object.Instantiate(artillery.Config.ProjectilePrefab, artillery.SpawnPointProvider.SpawnPoint)
                    .With(x => x.transform.position = artillery.SpawnPointProvider.SpawnPoint.position)
                    .LaunchTo(_target, onComplete: damageable => Explosion(artillery.Config, damageable));
                
                artillery.Timer.Time = artillery.Config.Cooldown;
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