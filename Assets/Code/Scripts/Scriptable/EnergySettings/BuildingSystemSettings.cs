using UnityEngine;

namespace SustainTheStrain.Scriptable.EnergySettings
{
    [CreateAssetMenu(fileName = "BuildingSystemSettings", menuName = "EnergySystemSettings/BuildingSystemSettings", order = 1)]
    public class BuildingSystemSettings : EnergySystemSettings
    {
        [field: SerializeField, Min(0.0f)] public float[] DamageMultipliers { get; private set; }
        [field: SerializeField, Min(0.0f)] public float[] CooldownMultipliers { get; private set; }

        private void OnValidate()
        {
            if (DamageMultipliers.Length != MaxEnergy + 1) DamageMultipliers = new float[MaxEnergy + 1];
            if (CooldownMultipliers.Length != MaxEnergy + 1) CooldownMultipliers = new float[MaxEnergy + 1];
        }
    }
}