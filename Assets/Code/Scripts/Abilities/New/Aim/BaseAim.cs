using UnityEngine;

namespace SustainTheStrain.Abilities.New
{
    public abstract class BaseAim
    {
        private readonly LayerMask _raycastMask;
        private readonly float _raycastDistance;

        protected BaseAim(LayerMask raycastMask, float raycastDistance)
        {
            _raycastMask = raycastMask;
            _raycastDistance = raycastDistance;
        }

        public bool TryRaycast(Ray ray, out RaycastHit hit) => Physics.Raycast(ray, out hit, _raycastDistance, _raycastMask);

        public abstract void UpdatePosition(RaycastHit hit);
        public abstract void SpawnAimZone();
        public abstract void Destroy();
    }
}