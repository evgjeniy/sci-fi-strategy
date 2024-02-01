using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class EnemyHackAbility : PointAbility
    {
        public EnemyHackAbility(EnemyHackAbilitySettings settings)
        {
            LoadingSpeed = settings.ReloadingSpeed;
            SetEnergySettings(settings.EnergySettings);
        }

        public override void Shoot(RaycastHit hit, int team)
        {
            if (!IsReloaded())
            {
                FailShootLogic();
                return;
            }
            var stdmg = hit.collider?.GetComponent<Units.Components.Damageble>();
            if (stdmg == null || stdmg.Team == team)
            {
                FailShootLogic();
                return;
            }
            Reload = 0;
            SuccessShootLogic(hit, team);
        }

        protected override void FailShootLogic()
        {
            Debug.Log("HACK failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            var dmg = hit.collider?.GetComponent<Units.Components.Damageble>();
            dmg.Team = team;
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("HACK ready to shoot");
        }
    }
}
