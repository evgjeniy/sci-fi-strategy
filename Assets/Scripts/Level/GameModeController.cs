using System;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Level
{
    public class GameModeController : MonoBehaviour
    {
        [Inject] private WavesManager _wavesManager;
        [Inject] private Citadel _citadel;
        private void OnEnable()
        {
            
        }

        private void Start()
        {
            _wavesManager.StartWaves();
        }
    }
}
