﻿namespace SustainTheStrain.Buildings
{
    public class ArtilleryIdleState : IUpdatableState<Artillery>
    {
        public IUpdatableState<Artillery> Update(Artillery artillery)
        {
            artillery.Area.Update(artillery.transform.position, artillery.Config.Radius, artillery.Config.Mask);

            foreach (var damageable in artillery.Area.Entities)
                return new ArtilleryAttackState(damageable);

            return this;
        }
    }
}