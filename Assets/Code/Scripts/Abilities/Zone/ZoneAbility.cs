using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain.AbilitiesScripts
{
    public abstract class ZoneAbility : BaseAbility
    {
        protected float zoneRadius;
        protected GameObject ExplosionPrefab;
        protected readonly Vector3 offset = new(0, 1.5f,0);

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
            if(ExplosionPrefab == null) return;
            var Explosion = GameObject.Instantiate(ExplosionPrefab, point + offset, Quaternion.identity);
            Explosion.transform.localScale = Vector3.one * 3f;
            Explosion.transform.localPosition += new Vector3(-0.7f, 0, 0.2f);
            GameObject.Destroy(Explosion, 3);
        }
    }
}
