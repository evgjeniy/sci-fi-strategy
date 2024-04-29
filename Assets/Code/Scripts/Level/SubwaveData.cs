using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Level
{
    [Serializable]
    public struct SubwaveData
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _spawnPeriod;
        [SerializeField] private int _enemyCount;
        [SerializeField] private List<GroupItem> _enemyGroup;

        public float spawnPeriod => _spawnPeriod;
        public int enemyCount => _enemyCount;
        public List<GroupItem> enemyGroup => _enemyGroup;
        public float delay => _delay;
    }

    [Serializable]
    public struct GroupItem
    {
        [SerializeField] private string _enemyType;
        [SerializeField] private float _xOffset;
        
        public string enemyType => _enemyType;
        public float xOffset => _xOffset;
    }
}
