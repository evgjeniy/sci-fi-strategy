namespace SustainTheStrain.Buildings
{
    public class LaserIdleState : IUpdatableState<Laser>
    {
        public IUpdatableState<Laser> Update(Laser laser)
        {
            laser.Area.Update(laser.transform.position, laser.Config.Radius, laser.Config.Mask);
            
            foreach (var damageable in laser.Area.Entities)
                return new LaserAttackState(damageable);

            return this;
        }
    }
}