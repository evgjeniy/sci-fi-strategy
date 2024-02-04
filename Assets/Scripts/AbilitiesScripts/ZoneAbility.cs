using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain.AbilitiesScripts
{
    public abstract class ZoneAbility : BaseAbility
    {
        protected float zoneRadius;
        protected GameObject ExplosionPrefab;
        protected readonly Vector3 offset = new(0, 0.5f, 0);

        protected Collider[] GetColliders(Vector3 point) => Physics.OverlapSphere(point, zoneRadius);

        public override void Shoot(RaycastHit hit, int team)
        {
            if (!IsReloaded())
            {
                FailShootLogic();
                return;
            }
            Reload = 0;
            SuccessShootLogic(hit, team);
            SpawnParticles(hit.point);
        }

        protected void SpawnParticles(Vector3 point)
        {
            var Explosion = GameObject.Instantiate(ExplosionPrefab, point + offset, Quaternion.identity);
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
    }
}
