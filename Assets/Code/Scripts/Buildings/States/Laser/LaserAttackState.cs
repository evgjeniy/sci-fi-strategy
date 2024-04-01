using System.Linq;
using Cysharp.Threading.Tasks;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class LaserAttackState : ILaserState
    {
        private const float LineVisualDuration = 0.5f;
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

            EnableLineAttack(laserData);
            
            return this;
        }

        private async void EnableLineAttack(LaserData laserData)
        {
            laserData.Line.Enable();

            for (var time = 0.0f; time < LineVisualDuration; time += Time.deltaTime)
            {
                var isLineNull = laserData.Line == null;

                if (_target == null)
                {
                    if (isLineNull) return;
                    break;
                }

                if (!isLineNull)
                {
                    laserData.Line.SetPositions(new[]
                    {
                        laserData.ProjectileSpawnPoint.position,
                        _target.transform.position
                    });
                }

                await UniTask.NextFrame();
            }

            laserData.Line.Disable();
        }
    }
}