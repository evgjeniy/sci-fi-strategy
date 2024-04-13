using UnityEngine;

namespace SustainTheStrain.Scriptable
{
    [CreateAssetMenu(fileName = "EnergyManagerUpgradeCostSettings", menuName = "UpgradeCostList/EnergyManager", order = 1)]
    public class EnergyManagerUpgradeCostList : ScriptableObject
    {
        [field: SerializeField] public int[] UpgradeCostList { get; private set; }
    }
}