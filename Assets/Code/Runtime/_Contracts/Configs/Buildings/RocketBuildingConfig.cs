using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(RocketBuildingConfig), menuName = "Configs/" + nameof(RocketBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class RocketBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(1)] public int MaxTargets { get; private set; } = 1;
        [field: SerializeField, Range(0.0f, 360.0f)] public float SectorAngle { get; private set; } = 45.0f;
        
        [field: Space]
        [field: SerializeField] public RocketBuildingConfig NextLevelConfig { get; private set; }
        
        public int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}