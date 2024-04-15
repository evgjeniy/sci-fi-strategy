namespace SustainTheStrain.Buildings
{
    public class RocketIdleState : IUpdatableState<Rocket>
    {
        public IUpdatableState<Rocket> Update(Rocket rocket)
        {
            rocket.Area.Update(rocket.transform.position, rocket.Config.Radius, rocket.Config.Mask);

            foreach (var damageable in rocket.Area.Entities)
                return new RocketRotationState(damageable, rocket.Orientation, rocket.transform.position);

            return this;
        }
    }
}