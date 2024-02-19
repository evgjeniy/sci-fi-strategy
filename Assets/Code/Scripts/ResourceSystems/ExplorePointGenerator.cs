using System;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.ResourceSystems
{
    public class ExplorePointGenerator : ResourceGenerator, IEnergySystem
    {
        [Inject] public EnergyController EnergyController { get; set; }

        [field: SerializeField] public EnergySystemSettings EnergySettings { get; private set; }
        public Sprite ButtonImage => EnergySettings.ButtonImage;
        public int EnergySpendCount => EnergySettings.EnergySpend;
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        private int _currentEnergy;
        private int _maxEnergy;

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

        public int MaxEnergy
        {
            get => _maxEnergy;
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
            if (FreeEnergyCells < EnergySpendCount) return;
            if (EnergyController.TryGetEnergy(EnergySpendCount))
            {
                CurrentEnergy += EnergySpendCount;
                UpgradeAll();
            }
        }

        public void TryRefillEnergy()
        {
            if (_currentEnergy < EnergySpendCount) return;
            if (EnergyController.TryReturnEnergy(EnergySpendCount))
            {
                CurrentEnergy -= EnergySpendCount;
                DowngradeAll();
            }
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