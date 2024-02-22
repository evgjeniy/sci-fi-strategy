using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;

namespace SustainTheStrain.Scriptable.AbilitySettings
{
    public abstract class AbilitySettings : ScriptableObject
    {
        [field: SerializeField] public EnergySystemSettings EnergySettings { get; private set; }
        [field: SerializeField] public float ReloadingSpeed { get; private set; }
    }
}
