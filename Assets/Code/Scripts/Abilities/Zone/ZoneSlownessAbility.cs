using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ZoneSlownessAbility : ZoneAbility
    {
        protected float speedCoefficient;
        protected float slownessTime;

        public ZoneSlownessAbility(ZoneSlownesAbillitySettings settings)
        {
            zoneRadius = settings.ZoneRadius;
            LoadingSpeed = settings.ReloadingSpeed;
            speedCoefficient = settings.SpeedKoeficient == 0 ? 1 : settings.SpeedKoeficient;
            slownessTime = settings.DurationTime;
            ExplosionPrefab = settings.ExplosionPrefab;
            SetEnergySettings(settings.EnergySettings);
        }

        protected override void FailShootLogic()
        {
            Debug.Log("zoneSLOW failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            Collider[] colliders = GetColliders(hit.point);
            for (int i = 0; i < colliders.Length; i++)
            {
                var spd = colliders[i]?.GetComponent<Units.Unit>()?.CurrentPathFollower;
                var dmg = colliders[i]?.GetComponent<Units.Components.Damageble>();
                if (spd == null || dmg == null || dmg.Team == team) continue;
                spd.Speed *= speedCoefficient;
                System.Timers.Timer timer = new System.Timers.Timer(slownessTime * 1000); // Convert seconds to milliseconds
                timer.AutoReset = false; // Make the timer fire only once
                timer.Elapsed += (sender, e) => RestoreSpeed(spd); // Set the event handler
                timer.Start();
                //Debug.Log(dmg.Speed);
            }
        }

        private void RestoreSpeed(Units.PathFollowers.IPathFollower spd)
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
