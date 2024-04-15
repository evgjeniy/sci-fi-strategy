using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Components;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class ZoneSlownessAbility : ZoneAbility
    {
        protected float speedCoefficient;
        protected float slownessTime;

        public ZoneSlownessAbility(ZoneSlownessAbilitySettings settings)
        {
            zoneRadius = settings.ZoneRadius;
            LoadingSpeed = settings.ReloadingSpeed;
            speedCoefficient = settings.SpeedMultiplier == 0 ? 1 : settings.SpeedMultiplier;
            slownessTime = settings.DurationTime;
            ExplosionPrefab = settings.ExplosionPrefab;
            SetEnergySettings(settings.EnergySettings);
        }

        protected override void FailShootLogic()
        {
            Debug.Log("zoneSLOW failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, Team team)
        {
            Collider[] colliders = GetColliders(hit.point);
            for (int i = 0; i < colliders.Length; i++)
            {
                var spd = colliders[i]?.GetComponent<Units.Unit>()?.CurrentPathFollower;
                var dmg = colliders[i]?.GetComponent<Damageble>();
                if (spd == null || dmg == null || dmg.Team == team) continue;

                spd.Speed *= speedCoefficient;
                RestoreSpeed(slownessTime, spd);
            }
        }

        private async void RestoreSpeed(float delay, IPathFollower spd)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Debug.Log("RESTORE");
            if (spd == null) return;
            spd.Speed /= speedCoefficient;
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneSLOW ready to shoot");
        }
    }
}
