using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Scriptable.Buildings
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Artillery", fileName = "Artillery")]
    public class ArtilleryData : BuildingData
    {
        [field: SerializeField] public Projectile Projectile { get; set; }
        [field: SerializeField] public PricedLevelStats<Stats>[] ArtilleryStats { get; private set; } = { new() };

        public void OnValidate()
        {
            if (ArtilleryStats.Length == 0)
                ArtilleryStats = new[] { new PricedLevelStats<Stats>() };
        }

        [System.Serializable]
        public class Stats : IStats
        {
            [field: SerializeField, Min(0.01f)] public float Damage { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackCooldown { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackRadius { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float ExplosionRadius { get; private set; } = 1.0f;
        }
    }
}