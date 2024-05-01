using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    [CreateAssetMenu(fileName = nameof(EnemyHackAbilityConfig), menuName = "Configs/" + nameof(EnemyHackAbilityConfig), order = Const.Order.AbilityConfigs)]
    public class EnemyHackAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public Team NewTeam { get; private set; }
        
        [field: Header("Raycast Settings")]
        [field: SerializeField] public float RaycastDistance { get; private set; } = float.MaxValue;
        [field: SerializeField] public LayerMask EnemyMask { get; private set; } = -1;
    }
}