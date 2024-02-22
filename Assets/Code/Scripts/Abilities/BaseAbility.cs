using System;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Abilities
{
    public abstract class BaseAbility : IEnergySystem
    {
        private const float Eps = 0.00001f;
        protected float Reload = 0f;
        protected float LoadingSpeed;
        
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

            if (!IsLoaded) return;
            Reload += LoadingSpeed * delt;
            if (IsReloaded())
                ReadyToShoot();
        }

        public bool IsReloaded() => GetReload() == 1f; //reload > 1 and pogreshnost ychtena v getReload()

        protected abstract void FailShootLogic();

        protected abstract void SuccessShootLogic(RaycastHit hit, int team);

        protected abstract void ReadyToShoot();
        public EnergySystemSettings EnergySettings { get; private set; }
        public Sprite ButtonImage { get; private set; }
        [Inject] public EnergyController EnergyController { get; set; }
        public int MaxEnergy { get; private set; }
        public int FreeEnergyCells => MaxEnergy - CurrentEnergy;
        public int CurrentEnergy 
        { 
            get => _currentEnergy;
            set
            {
                if (value < 0 || value > MaxEnergy) return;
                _currentEnergy = value;
                OnCurrentEnergyChanged?.Invoke(_currentEnergy);
                IsLoaded = value != 0;
                if (!IsLoaded)
                {
                    Reload = 0;
                }
            }
        }

        public bool IsLoaded
        {
            get;
            private set;
        }

        protected int _currentEnergy;
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        public event Action<IEnergySystem> OnEnergyAddRequire;
        public event Action<IEnergySystem> OnEnergyDeleteRequire;

        public void TrySpendEnergy()
        {
            Debug.Log("TryingSpend");
            OnEnergyAddRequire?.Invoke(this);
        }

        public void TryRefillEnergy()
        {
            Debug.Log("TryingReffil");
            OnEnergyDeleteRequire?.Invoke(this);
        }

        public void SetEnergySettings(EnergySystemSettings settings)
        {
            EnergySettings = settings;
            MaxEnergy = settings.MaxEnergy;
        }
    }
}
