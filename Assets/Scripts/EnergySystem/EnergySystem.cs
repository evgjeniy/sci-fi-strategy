using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Systems
{
    public class EnergySystem : MonoBehaviour, IEnergySystem
    {
        
        public EnergyManager _energyManager { get; set; }
        [field: SerializeField] public int _energySpendCount { get; set; }
        
        public int MaxEnergy
        {
            get => _maxEnergy;
            private set
            {
                if (value > _maxEnergy)
                {
                    _maxEnergy = value;
                    OnValueChanged?.Invoke(_maxEnergy);
                }
            }
        }

        public Action<int> OnValueChanged { get; set; }
        public Action<int> OnMaxValueChanged { get; set; }
        [SerializeField] 
        private int _maxEnergy;
        [SerializeField] 
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

        public void IncreaseMaxEnergy(int value)
        {
            MaxEnergy += value;
        }

        public void TrySpend()
        {
            if (_currentEnergy + _energySpendCount > MaxEnergy) return;
            if (_energyManager.TrySpend(_energySpendCount))
            {
                CurrentEnergy += _energySpendCount;
            }
            //Here should be manager upgrade logic
        }

        public void TryRefill()
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