using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ZoneDamageAbility : ZoneAbility
    {
        protected float damage;
        protected GameObject ExplosionPrefab;
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
            for(int i = 0; i < colliders.Length; i++)
            {
                var dmg = colliders[i]?.GetComponent<Units.Components.Damageble>();
                if (dmg == null || dmg.Team == team) continue;
                dmg.Damage(damage);
                //Debug.Log(dmg.CurrentHP);
            }
            var Explosion = GameObject.Instantiate(ExplosionPrefab, hit.transform, false);
            Explosion.SetActive(false);
            var ps = Explosion.GetComponent<ParticleSystem>();
            var main = ps.main;
            var sizeCurve = new ParticleSystem.MinMaxCurve();
            sizeCurve.mode = ParticleSystemCurveMode.TwoConstants;
            sizeCurve.constantMin = Mathf.Max(0, zoneRadius - 0.5f);
            sizeCurve.constantMax = zoneRadius + 0.5f;
            main.startSize = sizeCurve;
            Explosion.SetActive(true);
            GameObject.Destroy(Explosion, 1);
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneDMG ready to shoot");
        }
    }
}
