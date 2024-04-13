using System;
using SustainTheStrain.Scriptable;
using UnityEngine;

namespace SustainTheStrain.EnergySystem
{
    public class EnergyManager : MonoBehaviour
    {
        public event Action<int> OnEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        public event Action<int> OnUpgradeCostChanged;
        
        [SerializeField] private int _currentCount;
        [Min(1)] [SerializeField] private int _maxCount;
        private int _currentUpgradeLevel = 0;

        public int UpgradeCost => _upgradeCostSettings.UpgradeCostList[_currentUpgradeLevel];
        [SerializeField] private EnergyManagerUpgradeCostList _upgradeCostSettings;
        
        
        public int MaxCount { 
            get => _maxCount; 
            set
            {
                if (value <= 0) return;
                _maxCount = value;
                OnMaxEnergyChanged?.Invoke(_maxCount);
            } 
        }
        public int CurrentCount
        {
            get => _currentCount;
            private set
            {
                if (value < 0) return;
                _currentCount = value;
                OnEnergyChanged?.Invoke(_currentCount);
            }
        }
        
        private void OnEnable()
        {
            CurrentCount = _maxCount;
            OnUpgradeCostChanged?.Invoke(UpgradeCost);
        }

        public bool TrySpend(int value)
        {
            if (CurrentCount < value) return false;
            CurrentCount -= value;
            return true;
        }
        
        public bool TryRefill(int value)
        {
            if (CurrentCount + value > _maxCount) return false;
            CurrentCount += value;
            return true;
        }

        public void UpgradeEnergyCount()
        {
            _currentUpgradeLevel++;
            MaxCount++;
            OnUpgradeCostChanged?.Invoke(UpgradeCost);
        }
        
    }
}

