using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Systems
{
    public class EnergySpender : MonoBehaviour
    {
        [SerializeField] private EnergySystem _energySystem;
        [SerializeField] private int _energySpendCount;
        [SerializeField] private int _maxEnergy;
        public Action<int> OnValueChanged;
        private int _currentEnergy;
        public int CurrentEnergy
        {
            get => _currentEnergy;
            private set
            {
                if (value < 0) return;
                _currentEnergy = value;
                OnValueChanged?.Invoke(_currentEnergy);
            }
        }

        private void TrySpend()
        {
            if (_currentEnergy + _energySpendCount > _maxEnergy) return;
            if (_energySystem.TrySpend(_energySpendCount))
            {
                CurrentEnergy += _energySpendCount;
            }
            //Here should be system upgrade logic
        }

        private void TryRefill()
        {
            if (_currentEnergy <= 0) return;
            if (_energySystem.TryRefill(_energySpendCount))
            {
                CurrentEnergy -= _energySpendCount;
            }
        }

        //Energy spend test
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrySpend();
            }

            if (Input.GetMouseButtonDown(1))
            {
                TryRefill();
            }
        }
    }
}