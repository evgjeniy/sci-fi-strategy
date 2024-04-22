using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class LandingAbility : PointAbility
    {
        protected GameObject SquadPrefab;
        protected GameObject SpawnEffect;
        protected readonly Vector3 offset = new(0, 1f, 0);

        public LandingAbility(LandingAbilitySettings settings)
        {
            LoadingSpeed = settings.ReloadingSpeed;
            SquadPrefab = settings.Squad;
            SpawnEffect = settings.SpawnEffect;
            SetEnergySettings(settings.EnergySettings);
        }

        protected override void FailShootLogic()
        {
            Debug.Log("LAND failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit, Team team)
        {
            /*if (AbilitiesController.activeSquads.Count == AbilitiesController.maxSquads)
            {
                GameObject.Destroy(AbilitiesController.activeSquads[0]);
                AbilitiesController.activeSquads.RemoveAt(0);
            }*/

            var squad = GameObject.Instantiate(SquadPrefab, hit.point, Quaternion.identity);
            /*AbilitiesController.activeSquads.Add(squad);*/

            var group = squad.GetComponent<RecruitGroup>();
            group.GuardPost.Position = hit.point;
            
            group.OnGroupEmpty += () => { /*AbilitiesController.activeSquads.Remove(squad);*/ GameObject.Destroy(squad); };
            SpawnParticles(hit.point, group.GuardPost.Radius);
        }

        protected void SpawnParticles(Vector3 point, float radius)
        {
            var Effect = GameObject.Instantiate(SpawnEffect, point + offset, Quaternion.identity);
            Effect.SetActive(false);
            var ps = Effect.GetComponent<ParticleSystem>();
            var main = ps.main;
            var sizeCurve = new ParticleSystem.MinMaxCurve();
            sizeCurve.mode = ParticleSystemCurveMode.TwoConstants;
            sizeCurve.constantMin = Mathf.Max(0, radius + 1f);
            sizeCurve.constantMax = radius + 2f;
            main.startSize = sizeCurve;
            Effect.SetActive(true);
            GameObject.Destroy(Effect, 2);
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("LAND ready to shoot");
        }
    }
}
