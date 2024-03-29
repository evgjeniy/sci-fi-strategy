using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; }
    }
    
    public abstract class DamageAbilityConfig : AbilityConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; }
    }
}