using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public abstract class BaseAim
    {
        protected LayerMask layersToHit;
        protected int maxDistFromCamera;

        public void setLayersToHit(LayerMask lm) => layersToHit = lm;

        public void setMaxDistFromCamera(int dst) => maxDistFromCamera = dst;

        public RaycastHit? GetAimInfo(Ray ray) => Physics.Raycast(ray, out var hit, maxDistFromCamera, layersToHit) ? hit : null;

        public abstract void UpdateLogic(RaycastHit hit);

        public abstract void SpawnAimZone();

        public abstract void Destroy();
    }
}
