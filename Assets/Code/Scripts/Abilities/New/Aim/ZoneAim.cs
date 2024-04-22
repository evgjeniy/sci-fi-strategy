using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Abilities.New
{
    public class ZoneAim : BaseAim
    {
        private readonly ZoneVisualizer _aimPrefab;
        private readonly float _radius;

        private ZoneVisualizer _aimInstance;

        public ZoneAim(float radius, ZoneVisualizer aimPrefab, LayerMask mask, float distance) : base(mask, distance)
        {
            _radius = radius;
            _aimPrefab = aimPrefab;
        }

        public override void SpawnAimZone()
        {
            _aimInstance = Object.Instantiate(_aimPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
            _aimInstance.Radius = _radius;
        }

        public override void Destroy() => Object.Destroy(_aimInstance);

        public override void UpdatePosition(RaycastHit hit) => _aimInstance.transform.position = hit.point;

        public void DisplayReload(ITimer timer) => _aimInstance.IfNotNull(zone => zone.Color = timer.IsOver ? Color.green : Color.red);
    }
}