using SustainTheStrain.EnergySystem;
using UnityEngine;

namespace SustainTheStrain.Scriptable.EnergySettings
{
    [CreateAssetMenu(fileName = "EnergySystemSettings", menuName = "EnergySystemSettings/EnergySystemSettings", order = 1)]
    public class EnergySystemSettings : ScriptableObject
    {
        [field: NaughtyAttributes.ShowAssetPreview]
        [field: SerializeField] public Sprite ButtonImage { get; private set; }
        [field: SerializeField, Min(1)] public int EnergySpend { get; private set; }
        [field: SerializeField, Min(1)] public int MaxEnergy { get; set; }
        [field: SerializeField] public EnergySystemUIType SystemUIType { get; private set; }
    }
}