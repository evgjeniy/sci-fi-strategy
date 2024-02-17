using SustainTheStrain.AbilitiesScripts;
using SustainTheStrain.Units.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public override void UpdateLogic(RaycastHit hit)
        {

        }
    }
}
