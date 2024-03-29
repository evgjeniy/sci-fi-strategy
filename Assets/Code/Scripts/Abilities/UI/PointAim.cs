using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class PointAim : BaseAim
    {
        public PointAim(LayerMask lm, int dst)
        {
            layersToHit = lm;
            maxDistFromCamera = dst;
        }

        public override void Destroy()
        {
            
        }

        public override void SpawnAimZone()
        {
            
        }

        public override void UpdateLogic(RaycastHit hit)
        {

        }
    }
}
