using System;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Level
{
    [Serializable]
    public struct SubwaveData
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _spawnPeriod;
        [SerializeField] private int _enemyCount;
        [SerializeField] private string _enemyType;

        public float spawnPeriod => _spawnPeriod;
        public int enemyCount => _enemyCount;
        public string enemyType => _enemyType;
        public float delay => _delay;
    }
}
