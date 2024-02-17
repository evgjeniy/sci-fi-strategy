using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Level
{
    [Serializable]
    public struct WaveData
    {
        [SerializeField] public List<SpawnerPart> _spawners;

        public  List<SpawnerPart> SubwaveDatas => _spawners;
    }

    [Serializable]
    public struct SpawnerPart
    {
        [SerializeField] private float _delay;
        [SerializeField] private int _index;
        [SerializeField] private List<SubwaveData> _subwaves;
        
        public float delay => _delay;
        public int index => _index;
        public List<SubwaveData> subwaves => _subwaves;
    }
}
