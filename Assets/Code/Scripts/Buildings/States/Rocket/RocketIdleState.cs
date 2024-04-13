namespace SustainTheStrain.Buildings
{
    public class RocketIdleState : IUpdatableState<Rocket>
    {
        public IUpdatableState<Rocket> Update(Rocket rocket)
        {
            rocket.Area.Update(rocket.transform.position, rocket.Config.Radius, rocket.Config.Mask);
            
            foreach (var entity in rocket.Area.Entities)
                return new RocketAttackState(entity);

            return this;
        }
    }
}