using SustainTheStrain.Buildings.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    public abstract class BaseBuildingData<TPrefab, TStats> : ScriptableObject where TPrefab : BaseBuilding where TStats : new()
    {
        [field: SerializeField] public TPrefab BuildingPrefab { get; private set; }
        [field: SerializeField] public Mesh Mesh { get; private set; }
        [field: SerializeField] public PricedLevelStats<TStats>[] PricedStats { get; private set; } = { new() };

        private void OnValidate()
        {
            if (PricedStats.Length == 0)
                PricedStats = new[] { new PricedLevelStats<TStats>() };
        }
    }
    
    [System.Serializable]
    public class PricedLevelStats<TStats> where TStats : new()
    {
        [field: SerializeField] public TStats Stats { get; private set; }
        [field: SerializeField, Min(0)] public int NextLevelPrice { get; private set; } = 1;
        [field: SerializeField, Min(0)] public int DestroyCompensation { get; private set; } = 1;
    }
}