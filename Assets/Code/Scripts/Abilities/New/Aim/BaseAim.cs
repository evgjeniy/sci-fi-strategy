using UnityEngine;

namespace SustainTheStrain.Abilities.New
{
    public abstract class BaseAim
    {
        public LayerMask RaycastMask { get; protected set; }
        public float RaycastDistance { get; protected set; }

        protected BaseAim(LayerMask raycastMask, float raycastDistance)
        {
            RaycastMask = raycastMask;
            RaycastDistance = raycastDistance;
        }

        public bool TryRaycast(Ray ray, out RaycastHit hit) => Physics.Raycast(ray, out hit, RaycastDistance, RaycastMask);

        public abstract void UpdatePosition(RaycastHit hit);
        public abstract void SpawnAimZone();
        public abstract void Destroy();
    }
}