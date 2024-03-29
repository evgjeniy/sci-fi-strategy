using SustainTheStrain._Contracts;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(LaserBuildingConfig), menuName = "Configs/" + nameof(LaserBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class LaserBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        
        [field: Space, SerializeField] public BuildingRotator GfxPrefab { get; private set; }

        [field: Space, SerializeField] public LaserBuildingConfig NextLevelConfig { get; private set; }

        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}