﻿using UnityEngine;

namespace SustainTheStrain.Scriptable.Buildings
{
    public abstract class BuildingData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public Mesh Mesh { get; private set; }
        [field: SerializeField] public LayerMask AttackMask { get; private set; }
        [field: SerializeField] public int CreatePrice { get; private set; } 
        
        public interface IStats {}
    }

    [System.Serializable]
    public class PricedLevelStats<TStats> where TStats : BuildingData.IStats, new()
    {
        [field: SerializeField] public GameObject Graphics { get; private set; }
        [field: SerializeField] public TStats Stats { get; private set; }
        [field: SerializeField, Min(0)] public int NextLevelPrice { get; private set; } = 1;
        [field: SerializeField, Min(0)] public int DestroyCompensation { get; private set; } = 1;
    }
}