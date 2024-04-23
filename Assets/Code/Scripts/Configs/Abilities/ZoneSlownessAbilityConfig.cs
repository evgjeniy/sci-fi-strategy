using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    [CreateAssetMenu(fileName = nameof(ZoneSlownessAbilityConfig), menuName = "Configs/" + nameof(ZoneSlownessAbilityConfig), order = Const.Order.AbilityConfigs)]
    public class ZoneSlownessAbilityConfig : AbilityConfig
    {
        [field: SerializeField, Min(0.0f)] public float Radius { get; private set; } = 5.0f;
        [field: SerializeField, Range(0.0f, 1.0f)] public float SpeedMultiplier { get; set; } = 0.5f;
        [field: SerializeField] public float Duration { get; set; } = 1.5f;

        [field: Header("Prefabs")]
        [field: SerializeField] public ZoneVisualizer AimPrefab { get; private set; }

        [field: Header("Raycast Settings")]
        [field: SerializeField] public float RaycastDistance { get; private set; } = float.MaxValue;

        [field: SerializeField] public LayerMask GroundMask { get; private set; } = -1;
        [field: SerializeField] public LayerMask DamageMask { get; private set; } = -1;
    }
}