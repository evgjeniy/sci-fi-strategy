using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class EnemyHackAbility : PointAbility
    {
        protected override void FailShootLogic()
        {
            Debug.Log("HACK failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit)
        {
            Debug.Log("HACK success shot");
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("HACK ready to shoot");
        }
    }
}
