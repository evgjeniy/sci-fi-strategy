using System;
using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.Settings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain
{
    public class CitadelShieldController : MonoBehaviour, IEnergySystem
    {
        [SerializeField] private Shield _shield;
        
        public EnergyController EnergyController { get; set; }
        [field:SerializeField] public EnergySystemSettings EnergySettings { get; private set; }
        public Sprite ButtonImage => EnergySettings.ButtonImage;
        public int EnergySpendCount => EnergySettings.EnergySpend;
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        private int _currentEnergy;
        private int _maxEnergy;

        [Inject]
        private void Init(EnergyController controller)
        {
            EnergyController = controller;
            LoadSettings();
            EnergyController.AddEnergySystem(this);
            
        }
        
        public int CurrentEnergy
        {
            get => _currentEnergy;
            private set
            {
                if (value < 0 || value > MaxEnergy) return;
                _currentEnergy = value;
                _shield.CellsCount = _currentEnergy;
                OnCurrentEnergyChanged?.Invoke(_currentEnergy);
            }
        }
        public int MaxEnergy {
            get =>_maxEnergy;
            private set
            {
                _maxEnergy = value;
                OnMaxEnergyChanged?.Invoke(value);
            } 
        }

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
            }
        }

        public void TryRefillEnergy()
        {
            if (_currentEnergy < EnergySpendCount) return;
            if (EnergyController.TryReturnEnergy(EnergySpendCount))
            {
                CurrentEnergy -= EnergySpendCount;
            }
        }

        private void LoadSettings()
        {
            MaxEnergy = EnergySettings.MaxEnergy;
        }
    }
}
