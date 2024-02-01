using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class LandingAbility : PointAbility
    {
        protected GameObject SquadPrefab;

        public LandingAbility(LandingAbilitySettings settings)
        {
            LoadingSpeed = settings.ReloadingSpeed;
            SquadPrefab = settings.Squad;
            SetEnergySettings(settings.EnergySettings);
        }

        protected override void FailShootLogic()
        {
            Debug.Log("LAND failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            var squad = GameObject.Instantiate(SquadPrefab, hit.point, Quaternion.identity);
            var group = squad.GetComponent<RecruitGroup>();
            group.GuardPost.Position = hit.point;
            group.OnGroupEmpty += () => { GameObject.Destroy(squad); };
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("LAND ready to shoot");
        }
    }
}
