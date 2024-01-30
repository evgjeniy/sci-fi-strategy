using UnityEngine;

namespace SustainTheStrain.EnergySystem.Settings
{
    [CreateAssetMenu(fileName = "EnergySystemSettings", menuName = "EnergySystemSettings/EnergySystemSettings", order = 1)]
    public class EnergySystemSettings : ScriptableObject
    {
        public Sprite ButtonImage;
        public int EnergySpend;
        public int MaxEnergy;
    }
}