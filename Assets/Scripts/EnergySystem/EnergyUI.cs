using System;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class EnergyUI : MonoBehaviour
    {
        [SerializeField] private Slider _valueSlider;
        [SerializeField] private EnergySystem _energySystem;

        public void Bind(EnergySystem system)
        {
            _energySystem = system;
            _valueSlider.maxValue = system.MaxCount;
            _valueSlider.minValue = 0;
            _energySystem.OnValueChanged += Display;
        }
        
        private void Display(int value)
        {
            _valueSlider.value = value;
        }

        private void OnDisable()
        {
            _energySystem.OnValueChanged -= Display;
        }
    }
}