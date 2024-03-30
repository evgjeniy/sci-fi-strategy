using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class EnemyHackAbility : PointAbility
    {
        public EnemyHackAbility(EnemyHackAbilitySettings settings)
        {
            LoadingSpeed = settings.ReloadingSpeed;
            SetEnergySettings(settings.EnergySettings);
        }

        public override void Shoot(RaycastHit hit, Team team)
        {
            if (!IsReloaded())
            {
                FailShootLogic();
                return;
            }
            var stdmg = hit.collider?.GetComponent<Damageble>();
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

        protected override void SuccessShootLogic(RaycastHit hit, Team team)
        {
            var dmg = hit.collider?.GetComponent<Damageble>();
            dmg.Team = team;
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("HACK ready to shoot");
        }
    }
}
