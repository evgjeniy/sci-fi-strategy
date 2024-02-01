using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Laser", fileName = "Laser")]
    public class LaserData : BuildingData
    {
        [field: SerializeField] public PricedLevelStats<Stats>[] LaserStats { get; private set; }

        public void OnValidate()
        {
            if (LaserStats.Length == 0)
                LaserStats = new[] { new PricedLevelStats<Stats>() };
        }
        
        [System.Serializable]
        public class Stats : IStats
        {
            [field: SerializeField, Min(0.01f)] public float Damage { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackCooldown { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackRadius { get; private set; } = 1.0f;
        }
    }
}