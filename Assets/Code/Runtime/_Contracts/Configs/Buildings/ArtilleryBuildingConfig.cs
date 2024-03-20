using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(ArtilleryBuildingConfig), menuName = "Configs/" + nameof(ArtilleryBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class ArtilleryBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float ExplosionRadius { get; private set; } = 1.0f;
        
        [field: Space]
        [field: SerializeField] public ArtilleryBuildingConfig NextLevelConfig { get; private set; }
        
        public int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}