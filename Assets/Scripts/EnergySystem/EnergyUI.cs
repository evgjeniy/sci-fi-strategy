using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Systems
{
    public class EnergyUI : MonoBehaviour
    {
        [SerializeField] private Slider _valueSlider;
        [SerializeField] private EnergyManager energyManager;

        [Inject]
        public void Bind(EnergyManager manager)
        {
            energyManager = manager;
            _valueSlider.maxValue = manager.MaxCount;
            _valueSlider.minValue = 0;
            energyManager.OnValueChanged += Display;
        }
        
        private void Display(int value)
        {
            _valueSlider.value = value;
        }

        private void OnDisable()
        {
            energyManager.OnValueChanged -= Display;
        }
    }
}