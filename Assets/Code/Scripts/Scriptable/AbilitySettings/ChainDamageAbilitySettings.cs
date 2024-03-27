using UnityEngine;

namespace SustainTheStrain.Scriptable.AbilitySettings
{
    [CreateAssetMenu(fileName = "ChainAbilitySettings", menuName = "AbilitySettings/ChainAbility", order = 1)]
    public class ChainDamageAbilitySettings : AbilitySettings
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public int MaxTargets { get; private set; }
        [field: SerializeField] public int Distance { get; private set; }
        [field: SerializeField] public GameObject LinePrefab { get; private set; }
    }
}