using System;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.Settings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.AbilitiesScripts
{
    public abstract class BaseAbility : IEnergySystem
    {
        private const float Eps = 0.00001f;
        protected float Reload = 1f;
        protected float LoadingSpeed;
        private IEnergySystem _energySystemImplementation;

        public float GetReload() => Mathf.Abs(Reload - 1f) < Eps ? 1f : Mathf.Min(Reload, 1f);

        public void SetLoadingSpeed(float speed) => LoadingSpeed = speed; //ne znayu nuzhen li no pust budet))

        public virtual void Shoot(RaycastHit hit, int team)
        {
            if (!IsReloaded())
            {
                FailShootLogic();
                return;
            }
            Reload = 0;
            SuccessShootLogic(hit, team);
        }

        public void Load(float delt)
        {
            if (IsReloaded()) return;

            if (!_isLoaded) return;
            Reload += LoadingSpeed * delt;
            if (IsReloaded())
                ReadyToShoot();
        }

        public bool IsReloaded() => GetReload() == 1f; //reload > 1 and pogreshnost ychtena v getReload()

        protected abstract void FailShootLogic();

        protected abstract void SuccessShootLogic(RaycastHit hit, int team);

        protected abstract void ReadyToShoot();
        public Sprite ButtonImage { get; private set; }
        [Inject] public EnergyController EnergyController { get; set; }
        public int EnergySpendCount { get; private set; }
        [field: Min(1)] public int MaxEnergy { get; private set; }
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;
        public int CurrentEnergy 
        { 
            get => _currentEnergy;
            private set
            {
                if (value < 0 || value > MaxEnergy) return;
                _currentEnergy = value;
                OnCurrentEnergyChanged?.Invoke(_currentEnergy);
                _isLoaded = value != 0;
            }
        }

        protected bool _isLoaded;

        protected int _currentEnergy;
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;

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

        protected void SetEnergySettings(EnergySystemSettings settings)
        {
            ButtonImage = settings.ButtonImage;
            EnergySpendCount = settings.EnergySpend;
            MaxEnergy = settings.MaxEnergy;
        }
    }
}
