using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<WaveData> _waves;

        public List<WaveData> waves => _waves;
    }
}
