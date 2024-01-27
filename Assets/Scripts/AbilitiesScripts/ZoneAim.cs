using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain
{
    public class ZoneAim : BaseAim
    {
        protected float zoneRadius;
        protected GameObject aimZonePrefab;
        protected GameObject aimZone;
        protected Vector3 offset = new(0, 0.8f, 0);
        protected readonly Vector3 _nullVector = new(0, 0, 0);
        protected readonly float depth = 25f;

        public ZoneAim(float radius, GameObject pref, LayerMask lm, int dst)
        {
            zoneRadius = radius;
            aimZonePrefab = pref;
            layersToHit = lm;
            maxDistFromCamera = dst;
        }

        public void setZoneRadius(float radius) => zoneRadius = radius;

        public void setAimZonePrefab(GameObject pref) => aimZonePrefab = pref;

        public override void SpawnAimZone()
        {
            aimZone = Object.Instantiate(aimZonePrefab, _nullVector, Quaternion.Euler(90, 0, 0));
            aimZone.GetComponent<DecalProjector>().size = new Vector3(2 * zoneRadius, 2 * zoneRadius, depth);
        }

        public override void Destroy() => Object.Destroy(aimZone);

        public override void UpdateLogic(Vector3 point)
        {
            aimZone.transform.position = point + offset;
        }
    }
}
