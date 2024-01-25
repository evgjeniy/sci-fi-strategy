using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ZoneSlownessAbility : ZoneAbility
    {
        protected override void FailShootLogic()
        {
            Debug.Log("zoneSLOW failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit)
        {
            Debug.Log("zoneSLOW success shot");
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneSLOW ready to shoot");
        }
    }
}
