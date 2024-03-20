using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class ZoneDamageAbility : ZoneAbility
    {
        protected float damage;

        public ZoneDamageAbility(ZoneDamageAbilitySettings settings)
        {
            zoneRadius = settings.ZoneRadius;
            LoadingSpeed = settings.ReloadingSpeed;
            damage = settings.Damage;
            ExplosionPrefab = settings.ExplosionPrefab;
            SetEnergySettings(settings.EnergySettings);
        }

        protected override void FailShootLogic()
        {
            Debug.Log("zoneDGM failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            Collider[] colliders = GetColliders(hit.point);
            for (int i = 0; i < colliders.Length; i++)
            {
                var dmg = colliders[i]?.GetComponent<Damageble>();
                if (dmg == null || dmg.Team == team) continue;
                dmg.Damage(damage);
                //Debug.Log(dmg.CurrentHP);
            }
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneDMG ready to shoot");
        }
    }
}