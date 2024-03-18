using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(LaserBuildingConfig), menuName = "Configs/" + nameof(LaserBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class LaserBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
    }
}