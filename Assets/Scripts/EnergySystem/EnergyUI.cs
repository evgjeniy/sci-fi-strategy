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
            SetMaxEnergy(manager.MaxCount);
            _valueSlider.minValue = 0;
            energyManager.OnValueChanged += Display;
            energyManager.OnMaxValueChanged += SetMaxEnergy;
        }
        
        private void Display(int value)
        {
            _valueSlider.value = value;
        }

        private void SetMaxEnergy(int value)
        {
            _valueSlider.maxValue = value;
        }

        private void OnDisable()
        {
            energyManager.OnValueChanged -= Display;
            energyManager.OnMaxValueChanged -= SetMaxEnergy;
        }
    }
}