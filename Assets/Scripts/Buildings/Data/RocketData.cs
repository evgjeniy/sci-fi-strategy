using SustainTheStrain.Buildings.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Rocket", fileName = "Rocket")]
    public class RocketData : BuildingData
    {
        [field: SerializeField] public Projectile Projectile { get; private set; }
        [field: SerializeField] public PricedLevelStats<Stats>[] RocketStats { get; private set; }

        public void OnValidate()
        {
            if (RocketStats.Length == 0)
                RocketStats = new[] { new PricedLevelStats<Stats>() };
        }

        [System.Serializable]
        public class Stats
        {
            [field: SerializeField, Min(1)] public int MaxEnemiesTargets { get; set; } = 1;
            [field: SerializeField, Min(0.0f)] public float Damage { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.0f)] public float AttackCooldown { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.0f)] public float AttackRadius { get; private set; } = 1.0f;
            [field: SerializeField, Range(0.0f, 360.0f)] public float AttackSectorAngle { get; private set; } = 45.0f;
        }
    }
}