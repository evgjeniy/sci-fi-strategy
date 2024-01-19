using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Barrack", fileName = "Barrack")]
    public class BarrackData : BuildingData
    {
        [field: SerializeField] public PricedLevelStats<Stats>[] BarrackStats { get; private set; }

        public void OnValidate()
        {
            if (BarrackStats.Length == 0)
                BarrackStats = new[] { new PricedLevelStats<Stats>() };
        }
        
        [System.Serializable]
        public class Stats
        {
            [field: SerializeField, Min(0.01f)] public float UnitMaxHealth { get; private set; } = 100.0f;
            [field: SerializeField, Min(0.01f)] public float UnitAttackDamage { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float UnitAttackCooldown { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float RespawnCooldown { get; private set; } = 1.0f;
        }
    }
}