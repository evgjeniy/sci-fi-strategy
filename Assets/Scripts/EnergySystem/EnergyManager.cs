using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class EnergyManager : MonoBehaviour
    {
        [SerializeField] private int _maxCount;
        
        public int MaxCount { 
            get => _maxCount; 
            private set 
            {
                if (value > _maxCount)
                {
                    _maxCount = value;
                    OnMaxValueChanged?.Invoke(_maxCount);
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
                OnValueChanged?.Invoke(_currentCount);
            }
        }
        
        public Action<int> OnMaxValueChanged;
        public Action<int> OnValueChanged;

        private void OnEnable()
        {
            CurrentCount = _maxCount;
        }

        void Start()
        {
        
        }
        
        void Update()
        {
        
        }

        public void IncreaseMaxEnergy(int value)
        {
            MaxCount += value;
        }

        public bool TrySpend(int value)
        {
            if (CurrentCount >= value)
            {
                CurrentCount -= value;
                return true;
            }

            return false;
        }
        
        public bool TryRefill(int value)
        {
            if (CurrentCount + value <= _maxCount)
            {
                CurrentCount += value;
                return true;
            }

            return false;
        }
        
        

       
    }
}

