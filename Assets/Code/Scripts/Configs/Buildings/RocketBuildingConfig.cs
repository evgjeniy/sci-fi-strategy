using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(RocketBuildingConfig), menuName = "Configs/" + nameof(RocketBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class RocketBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(1)] public int MaxTargets { get; private set; } = 1;
        [field: SerializeField, Range(0.0f, 360.0f)] public float SectorAngle { get; private set; } = 45.0f;

        [field: Header("Prefabs")]
        [field: SerializeField] public Projectile ProjectilePrefab { get; set; }
        [field: SerializeField] public GameObject ShootEffect { get; private set; }

        [field: Header("Next Level")]
        [field: SerializeField, Expandable] public RocketBuildingConfig NextLevelConfig { get; private set; }
        
        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}