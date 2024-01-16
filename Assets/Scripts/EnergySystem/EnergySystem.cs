using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Systems
{
    public class EnergySystem : MonoBehaviour
    {
        [SerializeField] private EnergyManager _energyManager;
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

        [Inject]
        public void BindManager(EnergyManager manager)
        {
            _energyManager = manager;
        }

        private void TrySpend()
        {
            if (_currentEnergy + _energySpendCount > _maxEnergy) return;
            if (_energyManager.TrySpend(_energySpendCount))
            {
                CurrentEnergy += _energySpendCount;
            }
            //Here should be manager upgrade logic
        }

        private void TryRefill()
        {
            if (_currentEnergy <= 0) return;
            if (_energyManager.TryRefill(_energySpendCount))
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