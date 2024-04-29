using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(RocketBuildingConfig), menuName = "Configs/" + nameof(RocketBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class RocketBuildingConfig : BuildingConfig
    {
        [SerializeField, Min(0.0f)] private float _damage = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(1)] public int MaxTargets { get; private set; } = 1;
        [field: SerializeField, Range(0.0f, 360.0f)] public float SectorAngle { get; private set; } = 45.0f;
        [field: SerializeField] public float[] EnergyDamageMultipliers { get; private set; } = { 0.5f, 0.7f, 1.0f, 1.2f };
        [field: SerializeField, Expandable] public FirePassiveSkillConfig PassiveSkill { get; private set; }

        [field: Header("Prefabs")]
        [field: SerializeField] public Projectile ProjectilePrefab { get; set; }

        [field: Space, SerializeField] public RocketBuildingConfig NextLevelConfig { get; private set; }

        private static int _currentEnergy;
        
        public int CurrentEnergy
        {
            get => _currentEnergy;
            set => _currentEnergy = Mathf.Clamp(value, 0, EnergyDamageMultipliers.Length);
        }

        public float Damage => _damage * EnergyDamageMultipliers[CurrentEnergy];
        public bool HasPassiveSkill => PassiveSkill != null;
        public bool IsMaxEnergy => _currentEnergy == EnergyDamageMultipliers.Length - 1;
        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;
    }
}