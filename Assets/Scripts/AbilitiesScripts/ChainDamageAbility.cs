using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ChainDamageAbility : PointAbility
    {
        protected override void FailShootLogic()
        {
            Debug.Log("CHAIN failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            Debug.Log("CHAIN success shot");
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("CHAIN ready to shoot");
        }
    }
}
