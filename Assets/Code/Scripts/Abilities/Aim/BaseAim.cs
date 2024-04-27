using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class BaseAim
    {
        private readonly LayerMask _raycastMask;
        private readonly float _raycastDistance;

        public BaseAim(LayerMask raycastMask, float raycastDistance)
        {
            _raycastMask = raycastMask;
            _raycastDistance = raycastDistance;
        }

        public virtual bool TryRaycast(Ray ray, out RaycastHit hit) => Physics.Raycast(ray, out hit, _raycastDistance, _raycastMask);

        public virtual void UpdatePosition(RaycastHit hit) {}
        public virtual void SpawnAimZone() {}
        public virtual void Destroy() {}
    }
}