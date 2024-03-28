using System.Linq;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketAttackState : IRocketState
    {
        private readonly Damageble _target;
        private Quaternion _currentRotation;

        public RocketAttackState(Damageble target) => _target = target;

        public IRocketState Update(Rocket rocket)
        {
            var rocketData = rocket.Data;
            var rocketConfig = rocketData.Config.Value;
            var rocketTransform = rocket.transform;

            rocketData.Timer.Time -= Time.deltaTime;
            rocketData.Area.Update(rocketTransform.position, rocketConfig.Radius, rocketConfig.Mask);
            
            if (rocketData.Area.Entities.Contains(_target) is false)
                return new RocketIdleState();
            
            rocketData.Orientation.Value = _target.transform.position;

            if (!rocketData.Timer.IsTimeOver)
                return this;
           
            var attackedAmount = 0;

            foreach (var damageable in rocketData.Area.Entities)
            {
                if (attackedAmount >= rocketConfig.MaxTargets) break;

                if (damageable.Team == 1) continue;
                if (!IsInSector(rocket, damageable.transform)) continue;

                var projectile = Object.Instantiate(rocketConfig.ProjectilePrefab, rocketTransform.position, rocketTransform.rotation);
                projectile.LaunchTo(damageable, d => d.Damage(rocket.Data.Config.Value.Damage));

                attackedAmount++;
            }

            if (attackedAmount != 0)
                rocketData.Timer.Time = rocketConfig.Cooldown;

            return this;
        }

        private static bool IsInSector(Rocket rocket, Transform target)
        {
            var rocketPosition = rocket.transform.position;
            
            var currentDirection = rocket.Data.Orientation - rocketPosition;
            var targetDirection = target.position - rocketPosition;

            var angle = Vector3.Angle(currentDirection, targetDirection);
            return angle <= rocket.Data.Config.Value.SectorAngle * 0.5f;
        }
    }
}