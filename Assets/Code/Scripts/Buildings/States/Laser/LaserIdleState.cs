using SustainTheStrain.Buildings.States;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class LaserIdleState : IUpdatableState<Laser>
    {
        public IUpdatableState<Laser> Update(Laser laser)
        {
            var laserData = laser.Data;
            var laserConfig = laserData.Config.Value;
            
            laserData.Timer.Time -= Time.deltaTime;
            laserData.Area.Update(laser.transform.position, laserConfig.Radius, laserConfig.Mask);
            
            foreach (var damageable in laserData.Area.Entities)
                return new LaserAttackState(damageable);

            return this;
        }
    }
}