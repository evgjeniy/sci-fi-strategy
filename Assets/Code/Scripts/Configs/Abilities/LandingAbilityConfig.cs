using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    public class LandingAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public UnitType[] Units { get; private set; }
    }
}