using System.Collections.Generic;
using SustainTheStrain.Level;
using UnityEngine;

namespace SustainTheStrain.Scriptable
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<WaveData> _waves;

        public List<WaveData> waves => _waves;
    }
}
