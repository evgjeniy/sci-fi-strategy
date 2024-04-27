using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    [CreateAssetMenu(fileName = nameof(FreezeAbilityConfig), menuName = "Configs/" + nameof(FreezeAbilityConfig), order = Const.Order.AbilityConfigs)]
    public class FreezeAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public float Duration { get; private set; } = 2.0f;
        [field: SerializeField] public float Radius { get; private set; } = 1000.0f;
        
        [field: Header("Raycast Settings")]
        [field: SerializeField] public float RaycastDistance { get; set; } = float.MaxValue;
        [field: SerializeField] public LayerMask RaycastMask { get; private set; } = -1;
        [field: SerializeField] public LayerMask DamageMask { get; private set; } = -1;
    }
}