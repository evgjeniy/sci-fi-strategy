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

                Object.Instantiate(rocket.Config.ProjectilePrefab, rocket.SpawnPointProvider.SpawnPoint)
                    .With(x => x.transform.position = rocket.SpawnPointProvider.SpawnPoint.position)
                    .LaunchTo(target, onComplete: damageable =>
                    {
                        var currentEnergy = rocket.RocketSystem.CurrentEnergy;
                        var multiplier = rocket.RocketSystem.EnergySettings.GetDamageMultiplier(currentEnergy);
                        
                        damageable.Damage(rocket.Config.Damage * multiplier);

                        if (rocket.Config.NextLevelConfig != null) return;
                        if (rocket.AttackCounter % rocket.RocketSystem.EnergySettings.PassiveSkill.AttackFrequency != 0) return;
                        if (rocket.RocketSystem.CurrentEnergy != rocket.RocketSystem.MaxEnergy) return;
                        
                        rocket.RocketSystem.EnergySettings.PassiveSkill.EnableSkill(damageable.gameObject);
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