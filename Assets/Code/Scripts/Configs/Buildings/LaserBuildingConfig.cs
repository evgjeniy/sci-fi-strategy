using NaughtyAttributes;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(LaserBuildingConfig), menuName = "Configs/" + nameof(LaserBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class LaserBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        
        [field: Header("Prefabs")]
        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }

        [field: Header("Next Level")]
        [field: SerializeField, Expandable] public LaserBuildingConfig NextLevelConfig { get; private set; }
        
        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}