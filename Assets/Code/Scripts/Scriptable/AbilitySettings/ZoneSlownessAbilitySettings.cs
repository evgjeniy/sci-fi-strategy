using UnityEngine;

namespace SustainTheStrain.Scriptable.AbilitySettings
{
    [CreateAssetMenu(fileName = "ZoneSlownessAbilitySettings", menuName = "AbilitySettings/ZoneSlowness", order = 1)]
    public class ZoneSlownessAbilitySettings : AbilitySettings
    {
        [field: SerializeField] public float ZoneRadius { get; private set; }
        [field: SerializeField] public float SpeedMultiplier { get; private set; }
        [field: SerializeField] public float DurationTime { get; private set; }
        [field: SerializeField] public GameObject ExplosionPrefab { get; private set; }
    }
}