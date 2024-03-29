using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class LaserIdleState : ILaserState
    {
        public ILaserState Update(Laser laser)
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