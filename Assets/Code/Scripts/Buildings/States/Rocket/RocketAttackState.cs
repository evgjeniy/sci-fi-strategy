using NTC.Pool;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class RocketAttackState : IUpdatableState<Rocket>
    {
        private readonly Damageble _target;

        public RocketAttackState(Damageble target) => _target = target;

        public IUpdatableState<Rocket> Update(Rocket rocket)
        {
            rocket.Area.Update(rocket.transform.position, rocket.Config.Radius, rocket.Config.Mask);
            
            if (_target.IsNotIn(rocket.Area))
                return new RocketIdleState();
            
            rocket.Orientation = _target.transform.position;

            if (!rocket.Timer.IsOver)
                return this;
           
            var attackedAmount = 0;

            foreach (var target in rocket.Area.Entities)
            {
                if (attackedAmount >= rocket.Config.MaxTargets) break;
                if (!IsInSector(rocket, target.transform)) continue;

                NightPool.Spawn(rocket.Config.ProjectilePrefab)
                    .With(x => x.transform.position = rocket.SpawnPointProvider.SpawnPoint.position)
                    .LaunchTo(target, onComplete: damageable =>
                    {
                        damageable.Damage(rocket.Config.Damage * rocket.EnergySystem.DamageMultiplier);

                        if (rocket.Config.NextLevelConfig != null) return;
                        if (rocket.AttackCounter % rocket.EnergySystem.Settings.PassiveSkill.AttackFrequency != 0) return;
                        if (rocket.EnergySystem.CurrentEnergy != rocket.EnergySystem.MaxEnergy) return;
                        
                        rocket.EnergySystem.Settings.PassiveSkill.EnableSkill(damageable.gameObject);
                    });

                attackedAmount++;
            }

            if (attackedAmount != 0)
            {
                rocket.Timer.ResetTime(rocket.Config.Cooldown);
                rocket.AttackCounter++;
            }

            return this;
        }

        private static bool IsInSector(Rocket rocket, Transform target)
        {
            var rocketPosition = rocket.transform.position;
            
            var currentDirection = rocket.Orientation - rocketPosition;
            var targetDirection = target.position - rocketPosition;

            var angle = Vector3.Angle(currentDirection, targetDirection);
            return angle <= rocket.Config.SectorAngle * 0.5f;
        }
    }
}