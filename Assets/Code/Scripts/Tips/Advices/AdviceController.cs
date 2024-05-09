using System.Linq;
using SustainTheStrain.Level;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Tips
{
    public class AdviceController : MonoBehaviour
    {
        [SerializeField] private Canvas _backgroundCanvas;
        [SerializeField] private AdviceSettings[] _startWaveSettings;
        [SerializeField] private AdviceSettings[] _endWaveSettings;
        
        [Inject] private WavesManager _wavesManager;

        public Canvas BackgroundCanvas => _backgroundCanvas;

        private void Awake()
        {
            _wavesManager.OnWaveStarted += OnWaveStarted;
            _wavesManager.OnWaveEnded += OnWaveEnded;
        }

        private void OnDestroy()
        {
            _wavesManager.OnWaveStarted -= OnWaveStarted;
            _wavesManager.OnWaveEnded -= OnWaveEnded;
        }

        private void OnWaveStarted(int waveNumber)
        {
            _startWaveSettings
                .FirstOrDefault(adviceSettings => adviceSettings.waveNumber == waveNumber)
                ?.adviceSequence.Activate();
        }

        private void OnWaveEnded(int waveNumber)
        {
            _endWaveSettings
                .FirstOrDefault(adviceSettings => adviceSettings.waveNumber == waveNumber)
                ?.adviceSequence.Activate();
        }
    }

    [System.Serializable]
    public class AdviceSettings
    {
        public int waveNumber;
        public AdviceSequence adviceSequence;
    }
}