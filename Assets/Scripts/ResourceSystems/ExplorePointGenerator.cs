using System;
using SustainTheStrain.EnergySystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.ResourceSystems
{
    public class ExplorePointGenerator : ResourceGenerator, IEnergySystem
    {
        [field:SerializeField] public Sprite ButtonImage { get; private set; }
        [Inject] public EnergyController EnergyController { get; set; }
        [field:SerializeField] public int EnergySpendCount { get; private set; }
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
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        public int CurrentEnergy
        {
            get => _currentEnergy;
            private set
            {
                if (value < 0 || value > MaxEnergy) return;
                if (!_canGenerate && value > 0)
                {
                    StartGeneration();
                }
                _currentEnergy = value;
                OnCurrentEnergyChanged?.Invoke(_currentEnergy);
                _canGenerate = value != 0;
            }
        }
    
        private int _currentEnergy;
        
        
        
        public void IncreaseMaxEnergy(int value)
        {
            MaxEnergy += value;
        }

        public void TrySpendEnergy()
        {
            if (FreeEnergyCells<EnergySpendCount) return;
            if (EnergyController.TryGetEnergy(EnergySpendCount))
            {
                CurrentEnergy += EnergySpendCount;
                UpgradeAll();
            }
            //Here should be system upgrade logic
        }

        public void TryRefillEnergy()
        {
            if (_currentEnergy < EnergySpendCount) return;
            if (EnergyController.TryReturnEnergy(EnergySpendCount))
            {
                CurrentEnergy -= EnergySpendCount;
                DowngradeAll();
            }
            //Here should be system downgrade logic
        }
        
        private void OnDisable()
        {
            EndGeneration();
        }
    }
}