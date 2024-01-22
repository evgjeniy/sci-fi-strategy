﻿using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Rocket", fileName = "Rocket")]
    public class RocketData : BuildingData
    {
        [field: SerializeField] public PricedLevelStats<Stats>[] RocketStats { get; private set; }

        public void OnValidate()
        {
            if (RocketStats.Length == 0)
                RocketStats = new[] { new PricedLevelStats<Stats>() };
        }
        
        [System.Serializable]
        public class Stats
        {
            [field: SerializeField, Min(0.01f)] public float Damage { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackCooldown { get; private set; } = 1.0f;
            [field: SerializeField, Min(0.01f)] public float AttackRadius { get; private set; } = 1.0f;
            [field: SerializeField, Range(0.01f, 359.9f)] public float AttackSectorAngle { get; private set; } = 45.0f;
        }
    }
}