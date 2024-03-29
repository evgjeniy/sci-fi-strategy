using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryIdleState : IArtilleryState
    {
        public IArtilleryState Update(Artillery artillery)
        {
            var artilleryData = artillery.Data;
            var artilleryConfig = artilleryData.Config.Value;

            artilleryData.Timer.Time -= Time.deltaTime;
            artilleryData.Area.Update(artillery.transform.position, artilleryConfig.Radius, artilleryConfig.Mask);

            foreach (var damageable in artilleryData.Area.Entities)
                return new ArtilleryAttackState(damageable);

            return this;
        }
    }
}