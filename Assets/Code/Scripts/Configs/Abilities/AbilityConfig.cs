using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [field: NaughtyAttributes.Expandable]
        [field: SerializeField] public EnergySystemSettings EnergySettings { get; private set; }
        
        [field: Header("Stats")]
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; }
    }
}