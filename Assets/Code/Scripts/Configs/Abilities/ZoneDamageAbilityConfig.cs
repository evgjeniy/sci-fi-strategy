using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    [CreateAssetMenu(fileName = nameof(ZoneDamageAbilityConfig), menuName = "Configs/" + nameof(ZoneDamageAbilityConfig), order = Const.Order.AbilityConfigs)]
    public class ZoneDamageAbilityConfig : AbilityConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; } = 20.0f;
        [field: SerializeField, Min(0.0f)] public float Radius { get; private set; } = 5.0f;
        
        [field: Header("Prefabs")]
        [field: SerializeField] public ZoneVisualizer AimPrefab { get; private set; }
        [field: SerializeField] public GameObject ExplosionPrefab { get; private set; }
        
        [field: Header("Raycast Settings")]
        [field: SerializeField] public float RaycastDistance { get; private set; } = float.MaxValue;
        [field: SerializeField] public LayerMask GroundMask { get; private set; } = -1;
        [field: SerializeField] public LayerMask DamageMask { get; private set; } = -1;
    }
}