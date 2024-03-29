using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Abilities
{
    public class ZoneDamageAbilityConfig : DamageAbilityConfig
    {
        [field: SerializeField, Min(0.0f)] public float ZoneRadius { get; private set; }
    }
}