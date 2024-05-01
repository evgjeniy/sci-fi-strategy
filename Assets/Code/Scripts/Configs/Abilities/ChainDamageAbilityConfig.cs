using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    [CreateAssetMenu(fileName = nameof(ChainDamageAbilityConfig), menuName = "Configs/" + nameof(ChainDamageAbilityConfig), order = Const.Order.AbilityConfigs)]
    public class ChainDamageAbilityConfig : AbilityConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; }
        [field: SerializeField, Min(0)] public int MaxTargets { get; private set; }
        [field: SerializeField, Min(0)] public int Distance { get; private set; }

        [field: Header("Prefabs")]
        [field: SerializeField] public LineRenderer LinePrefab { get; private set; }

        [field: Header("Raycast Settings")]
        [field: SerializeField] public float RaycastDistance { get; private set; } = float.MaxValue;
        [field: SerializeField] public LayerMask DamageMask { get; private set; } = -1;

        [field: Header("Other Settings")]
        [field: SerializeField] public float LineVisibilityDuration { get; private set; } = 2.0f;
    }
}