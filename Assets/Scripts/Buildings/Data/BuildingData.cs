using SustainTheStrain.Buildings.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    public abstract class BuildingData : ScriptableObject
    {
        [field: SerializeField] public Building Prefab { get; private set; }
        [field: SerializeField] public Mesh Mesh { get; private set; }
    }
    
    [System.Serializable]
    public class PricedLevelStats<TStats>
    {
        [field: SerializeField] public TStats Stats { get; private set; }
        [field: SerializeField, Min(0)] public int NextLevelPrice { get; private  set; } = 1;
        [field: SerializeField, Min(0)] public int DestroyCompensation { get; private  set; } = 1;
    }
}