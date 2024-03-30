using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Components;
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
                System.Timers.Timer timer = new System.Timers.Timer(slownessTime * 1000); // Convert seconds to milliseconds
                timer.AutoReset = false; // Make the timer fire only once
                timer.Elapsed += (sender, e) => RestoreSpeed(spd); // Set the event handler
                timer.Start();
                //Debug.Log(dmg.Speed);
            }
        }

        private void RestoreSpeed(IPathFollower spd)
        {
            if (spd == null) return;
            spd.Speed /= speedCoefficient;
            //Debug.Log(dmg.Speed);
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneSLOW ready to shoot");
        }
    }
}
