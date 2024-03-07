using UnityEngine;

namespace SustainTheStrain.Scriptable.ResourceSettings
{
    [CreateAssetMenu(fileName = "GeneratorUpgradeStats", menuName = "Generator Settings/Generator Upgrade Stats", order = 1)]
    public class GeneratorUpgradeStats : ScriptableObject
    {
        [field: SerializeField] public float CooldownChange { get; private set; }
        [field: SerializeField] public int IncomeChange { get; private set; }
    }
}