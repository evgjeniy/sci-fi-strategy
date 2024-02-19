using System;
using SustainTheStrain.EnergySystem.Settings;
using UnityEngine;

namespace SustainTheStrain.EnergySystem
{
    public abstract class MonoEnergySystem : MonoBehaviour, IEnergySystem
    {
        [field: SerializeField] public EnergySystemSettings EnergySettings { get; protected set; }

        private int _maxEnergy;
        private int _currentEnergy;

        public virtual event Action<int> OnMaxEnergyChanged;
        public virtual event Action<IEnergySystem> OnEnergyAddRequire;
        public virtual event Action<IEnergySystem> OnEnergyDeleteRequire;
        public virtual event Action<int> OnCurrentEnergyChanged;

        public virtual EnergyController EnergyController { get; set; }
        public virtual Sprite ButtonImage => EnergySettings.ButtonImage;
        public virtual int FreeEnergyCells => MaxEnergy - CurrentEnergy;

        public virtual int MaxEnergy
        {
            get => EnergySettings.MaxEnergy;
            protected set
            {
                if (value < 1) return;
                OnMaxEnergyChanged?.Invoke(EnergySettings.MaxEnergy = value);
            }
        }

        public virtual int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                if (value < 0 || value > MaxEnergy) return;
                OnCurrentEnergyChanged?.Invoke(_currentEnergy = value);
            }
        }

        [Zenject.Inject]
        private void InjectEnergyController(EnergyController energyController)
        {
            EnergyController = energyController;
            EnergyController.AddEnergySystem(this);

            SetEnergySettings(EnergySettings);
        }

        public virtual void IncreaseMaxEnergy(int value = 1) => MaxEnergy += value;

        public virtual void TrySpendEnergy()
        {
            OnEnergyAddRequire?.Invoke(this);
        }

        public virtual void TryRefillEnergy()
        {
            OnEnergyDeleteRequire?.Invoke(this);
        }

        public virtual void SetEnergySettings(EnergySystemSettings settings) {}
    }
}