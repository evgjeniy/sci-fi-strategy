using UnityEngine;

namespace SustainTheStrain
{
    [CreateAssetMenu(fileName = "ChainAbilitySettings", menuName = "AbilitySettings/ChainAbility", order = 1)]
    public class ChainDamageAbilitySettings : AbilitySettings
    {
        public float Damage;
        public int MaxTargets;
        public int Distance;
        public GameObject LinePrefab;
    }
}