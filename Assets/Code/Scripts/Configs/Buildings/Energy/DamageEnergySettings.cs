using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    public class DamageEnergySettings : EnergySystemSettings
    {
        [field: SerializeField] public float[] EnergyDamageMultipliers { get; private set; } = { 0.5f, 0.7f, 1.0f, 1.2f };

        public float GetDamageMultiplier(int currentEnergy)
        {
            var clampEnergy = Mathf.Clamp(currentEnergy, 0, EnergyDamageMultipliers.Length);
            return EnergyDamageMultipliers[clampEnergy];
        }
    }
}