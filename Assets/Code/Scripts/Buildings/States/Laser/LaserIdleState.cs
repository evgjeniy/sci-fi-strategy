using SustainTheStrain.Buildings.States;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class LaserIdleState : IUpdatableState<Laser>
    {
        public IUpdatableState<Laser> Update(Laser laser)
        {
            laser.Timer.Time -= Time.deltaTime;
            laser.Area.Update(laser.transform.position, laser.Config.Radius, laser.Config.Mask);
            
            foreach (var damageable in laser.Area.Entities)
                return new LaserAttackState(damageable);

            return this;
        }
    }
}