using SustainTheStrain.Buildings.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Artillery", fileName = "Artillery")]
    public class ArtilleryData : BaseBuildingData<Artillery, ArtilleryData.Stats>
    {
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