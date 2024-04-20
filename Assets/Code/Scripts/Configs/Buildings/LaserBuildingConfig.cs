using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(LaserBuildingConfig), menuName = "Configs/" + nameof(LaserBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class LaserBuildingConfig : BuildingConfig
    {
        [SerializeField, Min(0.0f)] public float _damage = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        
        [field: Header("Prefabs")]
        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }
        [field: SerializeField] public float[] EnergyDamageMultipliers { get; private set; } = { 0.5f, 0.7f, 1.0f, 1.2f };

        [field: Space, SerializeField] public LaserBuildingConfig NextLevelConfig { get; private set; }
        
        private static int _currentEnergy;
        
        public int CurrentEnergy
        {
            get => _currentEnergy;
            set => _currentEnergy = Mathf.Clamp(value, 0, EnergyDamageMultipliers.Length);
        }

        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}