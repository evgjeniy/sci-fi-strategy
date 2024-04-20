using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    public class ChainDamageAbilityConfig : AbilityConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; }
        [field: SerializeField, Min(0)] public int MaxTargets { get; private set; }
        [field: SerializeField, Min(0)] public int Distance { get; private set; }
    }
}