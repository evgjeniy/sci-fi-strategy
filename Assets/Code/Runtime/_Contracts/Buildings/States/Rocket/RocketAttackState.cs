using System.Linq;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

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

            foreach (var target in rocketData.Area.Entities)
            {
                if (attackedAmount >= rocketConfig.MaxTargets) break;
                if (!IsInSector(rocket, target.transform)) continue;

                Object.Instantiate(rocketConfig.ProjectilePrefab, rocketData.ProjectileSpawnPoint)
                    .With(x => x.transform.position = rocketData.ProjectileSpawnPoint.position)
                    .LaunchTo(target, onComplete: x => x.Damage(rocket.Data.Config.Value.Damage));

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