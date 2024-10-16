﻿using System;
using SustainTheStrain.Scriptable.EnergySettings;
using TMPro;
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
        
        public virtual Sprite ButtonImage => EnergySettings.ButtonImage;

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
            SetEnergySettings(EnergySettings);

            energyController.AddEnergySystem(this);
        }

        public virtual bool TrySpendEnergy(int count)
        {
            CurrentEnergy += count;
            return true;
        }

        //используй эти методы вместо прямого назначения
        public virtual bool TryRefillEnergy(int count)
        {
            CurrentEnergy -= count;
            return true;
        }

        public abstract void CacheUiTip(TMP_Text uiTip);

        public virtual void SetEnergySettings(EnergySystemSettings settings) {}
    }
}