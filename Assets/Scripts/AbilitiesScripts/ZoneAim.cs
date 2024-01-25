using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain
{
    public class ZoneAim
    {
        private float zoneRadius;
        private GameObject aimZonePrefab;
        private GameObject aimZone;
        private LayerMask layersToHit;
        private int maxDistFromCamera;
        private Vector3 offset = new(0, 0.8f, 0);
        private readonly Vector3 _nullVector = new(0, 0, 0);
        private readonly float depth = 25f;

        public ZoneAim(float radius, GameObject pref, LayerMask lm, int dst)
        {
            zoneRadius = radius;
            aimZonePrefab = pref;
            layersToHit = lm;
            maxDistFromCamera = dst;
        }

        public void setZoneRadius(float radius) => zoneRadius = radius;

        public void setAimZonePrefab(GameObject pref) => aimZonePrefab = pref;

        public void setLayersToHit(LayerMask lm) => layersToHit = lm;

        public void setMaxDistFromCamera(int dst) => maxDistFromCamera = dst;

        public void SpawnAimZone()
        {
            aimZone = Object.Instantiate(aimZonePrefab, _nullVector, Quaternion.Euler(90, 0, 0));
            aimZone.GetComponent<DecalProjector>().size = new Vector3(zoneRadius, zoneRadius, depth);
        }

        public RaycastHit? GetAimInfo(Ray ray) => Physics.Raycast(ray, out var hit, maxDistFromCamera, layersToHit) ? hit : null;

        public void Destroy() => Object.Destroy(aimZone);

        public void UpdateLogic(Vector3 point)
        {
            aimZone.transform.position = point + offset;
        }
    }
}
