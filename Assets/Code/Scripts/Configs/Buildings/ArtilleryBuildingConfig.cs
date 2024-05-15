using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(ArtilleryBuildingConfig), menuName = "Configs/" + nameof(ArtilleryBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class ArtilleryBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float ExplosionRadius { get; private set; } = 1.0f;
        
        [field: Header("Prefabs")]
        [field: SerializeField] public BuildingRotator GfxPrefab { get; private set; }
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
        [field: SerializeField] public GameObject ShootEffect { get; private set; }
        

        [field: Header("Next Level")]
        [field: SerializeField, Expandable] public ArtilleryBuildingConfig NextLevelConfig { get; private set; }

        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}