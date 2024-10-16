using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(BarrackBuildingConfig), menuName = "Configs/" + nameof(BarrackBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class BarrackBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.01f)] public float UnitMaxHealth { get; private set; } = 100.0f;
        [field: SerializeField, Min(0.01f)] public float UnitAttackDamage { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.01f)] public float UnitAttackCooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.01f)] public float RespawnCooldown { get; private set; } = 1.0f;

        [field: Header("Prefabs")]
        [field: SerializeField] public ZoneVisualizer RecruitSpawnAimPrefab { get; private set; }

        [field: Header("Next Level")]
        [field: SerializeField, Expandable] public BarrackBuildingConfig NextLevelConfig { get; private set; }

        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}