using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class EnergyManager : MonoBehaviour
    {
        public event Action<int> OnEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        
        [Min(1)] [SerializeField] private int _maxCount;
        public int MaxCount { 
            get => _maxCount; 
            private set 
            {
                if (value > 0)
                {
                    _maxCount = value;
                    OnMaxEnergyChanged?.Invoke(_maxCount);
                }
            } 
        }

        [SerializeField] private int _currentCount;
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
        }
        
        public void IncreaseMaxEnergy(int value)
        {
            MaxCount += value;
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
        
    }
}

