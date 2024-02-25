using System;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;

namespace SustainTheStrain.EnergySystem
{
    public abstract class MonoEnergySystem : MonoBehaviour, IEnergySystem
    {
        [field: SerializeField] public EnergySystemSettings EnergySettings { get; protected set; }

        protected int _maxEnergy;
        protected int _currentEnergy;
        
        public virtual event Action<int> OnCurrentEnergyChanged;
        public event Action<IEnergySystem> Changed;
        
        public virtual EnergyController EnergyController { get; set; }
        public virtual Sprite ButtonImage => EnergySettings.ButtonImage;
        public virtual int FreeEnergyCellsCount => MaxEnergy - CurrentEnergy;

        public virtual int MaxEnergy
        {
            get => _maxEnergy;
            set
            {
                if (value < 1) return;
                _maxEnergy = value;
                Changed?.Invoke(this);
            }
            
        }

        public virtual int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                if (value < 0 || value > MaxEnergy) return;
                _currentEnergy = value;
                Changed?.Invoke(this);
            }
        }

        [Zenject.Inject]
        private void Construct(EnergyController energyController)
        {
            EnergyController = energyController;
            EnergyController.AddEnergySystem(this);

            SetEnergySettings(EnergySettings);
        }

        public virtual bool TrySpendEnergy()
        {
            //logic
            return true;
        }

        public virtual bool TryRefillEnergy()
        {
            //logic
            return true;
        }

        public virtual void SetEnergySettings(EnergySystemSettings settings) {}
    }
}