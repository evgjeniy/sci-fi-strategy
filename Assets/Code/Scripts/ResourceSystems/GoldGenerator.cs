using System;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.ResourceSystems
{
    public class GoldGenerator : ResourceGenerator, IEnergySystem
    {
        [Inject] public EnergyController EnergyController { get; set; }
        
        [field:SerializeField] public EnergySystemSettings EnergySettings { get; private set; }
        public Sprite ButtonImage => EnergySettings.ButtonImage;
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        public event Action<IEnergySystem> OnEnergyAddRequire;
        public event Action<IEnergySystem> OnEnergyDeleteRequire;
        
        private int _currentEnergy;
        private int _maxEnergy;
        
        public int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                if (value < 0 || value > MaxEnergy) return;
                if (!_canGenerate && value > 0)
                {
                    StartGeneration();
                }

                if (value > _currentEnergy)
                {
                    UpgradeAll();
                }

                if (value < _currentEnergy)
                {
                    DowngradeAll();
                }
                _currentEnergy = value;
                OnCurrentEnergyChanged?.Invoke(_currentEnergy);
                _canGenerate = value != 0;
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

        private void OnEnable()
        {
            LoadSettings();
        }

        public void IncreaseMaxEnergy(int value)
        {
            MaxEnergy += value;
        }

        public void TrySpendEnergy()
        {
            OnEnergyAddRequire?.Invoke(this);
        }

        public void TryRefillEnergy()
        {
            OnEnergyDeleteRequire?.Invoke(this);
        }
        
        private void OnDisable()
        {
            EndGeneration();
        }

        public override void LoadSettings()
        {
            base.LoadSettings();
            MaxEnergy = EnergySettings.MaxEnergy;
        }
    }
}