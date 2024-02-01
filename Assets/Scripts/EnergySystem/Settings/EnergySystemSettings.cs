using UnityEngine;

namespace SustainTheStrain.EnergySystem.Settings
{
    [CreateAssetMenu(fileName = "EnergySystemSettings", menuName = "EnergySystemSettings/EnergySystemSettings", order = 1)]
    public class EnergySystemSettings : ScriptableObject
    {
        public Sprite ButtonImage;
        [field: Min(1)] public int EnergySpend;
        [field: Min(1)] public int MaxEnergy;
    }
}