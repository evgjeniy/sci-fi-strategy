using UnityEngine;

namespace SustainTheStrain.Scriptable.AbilitySettings
{
    [CreateAssetMenu(fileName = "LandingAbilitySettings", menuName = "AbilitySettings/LandingAbility", order = 1)]
    public class LandingAbilitySettings : AbilitySettings
    {
        [field: SerializeField] public GameObject Squad { get; private set; }
        [field: SerializeField] public GameObject SpawnEffect { get; private set; }
    }
}