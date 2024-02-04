using UnityEngine;

namespace SustainTheStrain
{
    [CreateAssetMenu(fileName = "LandingAbilitySettings", menuName = "AbilitySettings/LandingAbility", order = 1)]
    public class LandingAbilitySettings : AbilitySettings
    {
        public GameObject Squad;
        public GameObject SpawnEffect;
    }
}