using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Abilities
{
    public class LandingAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public UnitType[] Units { get; private set; }
    }
}