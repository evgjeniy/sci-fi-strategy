using System.Linq;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class LaserAttackState : ILaserState
    {
        private readonly Damageble _target;

        public LaserAttackState(Damageble target) => _target = target;

        public ILaserState Update(Laser laser)
        {
            var laserData = laser.Data;
            var laserConfig = laserData.Config.Value;

            laserData.Timer.Time -= Time.deltaTime;
            laserData.Area.Update(laser.transform.position, laserConfig.Radius, laserConfig.Mask);
            
            if (laserData.Area.Entities.Contains(_target) is false)
                return new LaserIdleState();
            
            laserData.Orientation.Value = _target.transform.position;
            if (!laserData.Timer.IsTimeOver) return this;
            
            _target.Damage(laserConfig.Damage);
            laserData.Timer.Time = laserConfig.Cooldown;

            return this;
        }
    }
}