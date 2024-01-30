using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Level
{
    public class WaveLabelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [Inject] private WavesManager _wavesManager;

        private void OnEnable()
        {
            _wavesManager.OnWaveStarted += UpdateLabel;
        }

        private void OnDisable()
        {
            _wavesManager.OnWaveStarted -= UpdateLabel;
        }

        private void UpdateLabel(int value)
        {
            _textMesh.text = value.ToString();
        }
    }
}
