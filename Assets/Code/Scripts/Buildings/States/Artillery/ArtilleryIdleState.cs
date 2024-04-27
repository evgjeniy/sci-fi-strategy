namespace SustainTheStrain.Buildings
{
    public class ArtilleryIdleState : IUpdatableState<Artillery>
    {
        public IUpdatableState<Artillery> Update(Artillery artillery)
        {
            artillery.Area.Update(artillery.transform.position, artillery.Config.Radius, artillery.Config.Mask);

            foreach (var damageable in artillery.Area.Entities)
            {
                if (damageable.IsFlying) continue;

                return new ArtilleryRotationState(damageable, artillery.Orientation, artillery.transform.position);
            }

            return this;
        }
    }
}