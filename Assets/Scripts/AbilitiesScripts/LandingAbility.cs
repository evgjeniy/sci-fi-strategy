using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class LandingAbility : PointAbility
    {
        protected int squadSize;

        public LandingAbility(LandingAbilitySettings settings)
        {
            LoadingSpeed = settings.ReloadingSpeed;
            squadSize = settings.Size;
        }

        protected override void FailShootLogic()
        {
            Debug.Log("LAND failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            //ifactory<squad>   there will be spawn
            //   +setting team to squad units
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("LAND ready to shoot");
        }
    }
}
