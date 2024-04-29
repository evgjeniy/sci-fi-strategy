using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(ArtilleryBuildingConfig), menuName = "Configs/" + nameof(ArtilleryBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class ArtilleryBuildingConfig : BuildingConfig
    {
        [SerializeField, Min(0.0f)] private float _damage = 1.0f;
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.0f)] public float ExplosionRadius { get; private set; } = 1.0f;
        
        [field: Header("Prefabs")]
        [field: SerializeField] public BuildingRotator GfxPrefab { get; private set; }
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
        [field: SerializeField] public float[] EnergyDamageMultipliers { get; private set; } = { 0.5f, 0.7f, 1.0f, 1.2f };
        [field: SerializeField, Expandable] public StunConfig PassiveSkill { get; private set; }

        [field: Space, SerializeField] public ArtilleryBuildingConfig NextLevelConfig { get; private set; }
        
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