using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
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

        public override void UpdateLogic(Vector3 point)
        {
            
        }
    }
}
