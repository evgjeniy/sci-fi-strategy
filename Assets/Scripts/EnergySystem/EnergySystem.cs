using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Systems
{
    public class EnergySystem : MonoBehaviour, IEnergySystem
    {
        
        public EnergyController EnergyController { get; set; }
        [field: SerializeField] public int EnergySpendCount { get; set; }
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;

        public int MaxEnergy
        {
            get => _maxEnergy;
            private set
            {
                _maxEnergy = value;
                OnMaxEnergyChanged?.Invoke(_maxEnergy);
            }
        }
        
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
                OnCurrentEnergyChanged?.Invoke(_currentEnergy);
            }
        }

        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;

        [Inject]
        public void BindController(EnergyController controller)
        {
            EnergyController = controller;
        }

        public void IncreaseMaxEnergy(int value)
        {
            MaxEnergy += value;
        }

        public void TrySpendEnergy()
        {
            if (_currentEnergy + EnergySpendCount > MaxEnergy) return;
            if (EnergyController.TryGetEnergy(EnergySpendCount))
            {
                CurrentEnergy += EnergySpendCount;
            }
            //Here should be system upgrade logic
        }

        public void TryRefillEnergy()
        {
            if (_currentEnergy <= 0) return;
            if (EnergyController.TryReturnEnergy(EnergySpendCount))
            {
                CurrentEnergy -= EnergySpendCount;
            }
            //Here should be system downgrade logic
        }

        //Energy spend test
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrySpendEnergy();
            }

            if (Input.GetMouseButtonDown(1))
            {
                TryRefillEnergy();
            }
        }
    }
}