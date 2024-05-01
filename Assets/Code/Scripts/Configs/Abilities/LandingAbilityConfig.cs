using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Configs.Abilities
{
    [CreateAssetMenu(fileName = nameof(LandingAbilityConfig), menuName = "Configs/" + nameof(LandingAbilityConfig), order = Const.Order.AbilityConfigs)]
    public class LandingAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public int MaxSquads { get; private set; } = 2;

        [field: Header("Prefabs")]
        [field: SerializeField] public ZoneVisualizer AimPrefab { get; private set; }
        [field: SerializeField] public RecruitGroup SquadPrefab { get; private set; }
        [field: SerializeField] public GameObject SpawnEffect { get; private set; }
        
        [field: Header("Raycast Settings")]
        [field: SerializeField] public float RaycastDistance { get; private set; } = float.MaxValue;
        [field: SerializeField] public LayerMask GroundMask { get; private set; } = -1;
    }
}