using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class EnergyManager : MonoBehaviour
    {
        [SerializeField] private int _maxCount;
        public int MaxCount => _maxCount;

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

        //public int FreeCount => _maxCount - _currentCount;
        public Action<int> OnValueChanged;

        private void OnEnable()
        {
            CurrentCount = _maxCount;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
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

