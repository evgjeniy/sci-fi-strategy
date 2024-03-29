using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class RocketIdleState : IRocketState
    {
        public IRocketState Update(Rocket rocket)
        {
            var rocketData = rocket.Data;
            var rocketConfig = rocketData.Config.Value;
            
            rocketData.Timer.Time -= Time.deltaTime;
            rocketData.Area.Update(rocket.transform.position, rocketConfig.Radius, rocketConfig.Mask);
            
            foreach (var entity in rocketData.Area.Entities)
                return new RocketAttackState(entity);

            return this;
        }
    }
}