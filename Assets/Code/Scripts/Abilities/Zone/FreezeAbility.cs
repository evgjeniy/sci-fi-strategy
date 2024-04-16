using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SustainTheStrain
{
    public class FreezeAbility : Abilities.ZoneAbility
    {
        protected float freezeTime;

        public FreezeAbility(ZoneSlownessAbilitySettings settings)
        {
            zoneRadius = settings.ZoneRadius;
            LoadingSpeed = settings.ReloadingSpeed;
            freezeTime = settings.DurationTime;
            ExplosionPrefab = settings.ExplosionPrefab;
            SetEnergySettings(settings.EnergySettings);
        }

        protected override void FailShootLogic()
        {
            Debug.Log("FREEZE failed to shoot");
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("FREEZE ready to shoot");
        }

        protected override async void SuccessShootLogic(RaycastHit hit, Team team)
        {
            Collider[] colliders = GetColliders(Vector3.zero);
            float[] oldSpeeds = new float[colliders.Length];

            for (int i = 0; i < colliders.Length; i++)
            {
                var unit = colliders[i]?.GetComponent<Units.Unit>();
                var dmg = colliders[i]?.GetComponent<Damageble>();

                if (unit == null || dmg == null || dmg.Team == team) continue;

                oldSpeeds[i] = unit.CurrentPathFollower.Speed;
                unit.Freeze();
            }

            await Task.Delay(TimeSpan.FromSeconds(freezeTime));

            for (int i = 0; i < colliders.Length; i++)
            {
                var unit = colliders[i]?.GetComponent<Units.Unit>();
                var dmg = colliders[i]?.GetComponent<Damageble>();

                if (unit == null || dmg == null || dmg.Team == team) continue;

                unit.Unfreeze(oldSpeeds[i]);
            }
        }
    }
}
