using UnityEngine;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    [CreateAssetMenu(fileName = "EnergySystemUISettings", menuName = "EnergySystemSettings/TypeUISettings", order = 1)]
    public class EnergySystemUISettings : ScriptableObject
    {
        [field: SerializeField] public Transform SpawnParent { get; private set; }
        [field: SerializeField] public EnergySystemUI UIPrefab { get; private set; }
        [field: SerializeField] public EnergySystemUIType UIType { get; private set; }
    }
}