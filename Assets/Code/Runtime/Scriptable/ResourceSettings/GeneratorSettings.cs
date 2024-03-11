using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.Scriptable.ResourceSettings
{
    [CreateAssetMenu(fileName = "GeneratorSettings", menuName = "Generator Settings/Generator Settings", order = 1)]
    public class GeneratorSettings : ScriptableObject
    {
        [field: SerializeField] public Image GeneratingIndicator { get; private set; }
        [field: SerializeField] public GeneratorUpgradeStats UpgradeStats { get; private set; }
        [field: SerializeField] public float BaseCooldown { get; private set; }
        [field: SerializeField] public float MinimalCooldown { get; private set; }
        [field: SerializeField] public float MaximalCooldown { get; private set; }
        [field: SerializeField] public int GenerateCount { get; private set; }
    }
}