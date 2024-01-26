using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class LandingAbility : PointAbility
    {
        protected override void FailShootLogic()
        {
            Debug.Log("LAND failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            Debug.Log("LAND success shot");
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("LAND ready to shoot");
        }
    }
}
