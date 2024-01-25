using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Artillery", fileName = "Artillery")]
    public class ArtilleryData : BuildingData
    {
        [field: SerializeField] public PricedLevelStats<Stats>[] ArtilleryStats { get; private set; } = { new() };

        public void OnValidate()
        {
            if (ArtilleryStats.Length == 0)
                ArtilleryStats = new[] { new PricedLevelStats<Stats>() };
        }

        [System.Serializable]
        public class Stats
        {
            [field: SerializeField, Min(0.01f)] public float Damage { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackCooldown { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackRadius { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float ExplosionRadius { get; private set; } = 1.0f;
        }
    }
}