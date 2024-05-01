using NaughtyAttributes;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    [CreateAssetMenu(fileName = nameof(BarrackBuildingConfig), menuName = "Configs/" + nameof(BarrackBuildingConfig), order = Const.Order.BuildingConfigs)]
    public class BarrackBuildingConfig : BuildingConfig
    {
        [field: SerializeField, Min(0.01f)] public float UnitMaxHealth { get; private set; } = 100.0f;
        [field: SerializeField, Min(0.01f)] public float UnitAttackDamage { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.01f)] public float UnitAttackCooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(0.01f)] public float RespawnCooldown { get; private set; } = 1.0f;
        [field: SerializeField, Min(0)] public int MaxEnergy { get; private set; } = 3;
        [field: SerializeField, Expandable] public AdditionalBarrackRecruitConfig PassiveSkill { get; set; }


        [field: Header("Prefabs")]
        [field: SerializeField] public ZoneVisualizer RecruitSpawnAimPrefab { get; private set; }

        [field: Header("Next Level")]
        [field: SerializeField, Expandable] public BarrackBuildingConfig NextLevelConfig { get; private set; }

        public override int NextLevelPrice => NextLevelConfig == null ? int.MaxValue : NextLevelConfig.Price;

        private static int _currentEnergy;

        public int CurrentEnergy
        {
            get => _currentEnergy;
            set => _currentEnergy = Mathf.Clamp(value, 0, MaxEnergy);
        }

        public bool HasPassiveSkill => PassiveSkill != null;

        public bool IsMaxEnergy => _currentEnergy == MaxEnergy - 1;
    }
}