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
        [SerializeField] private EnergyController _energyController;

        [Inject]
        public void Bind(EnergyController controller)
        {
            _energyController = controller;
            _valueSlider.minValue = 0;
            var manager = _energyController.Manager;
            _valueSlider.maxValue = manager.MaxCount;
            manager.OnEnergyChanged += Display;
            manager.OnMaxEnergyChanged += SetMaxEnergy;
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
            var manager = _energyController.Manager;
            manager.OnEnergyChanged -= Display;
            manager.OnMaxEnergyChanged -= SetMaxEnergy;
        }
    }
}