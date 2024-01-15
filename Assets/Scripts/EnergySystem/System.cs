using System;
using UnityEngine;

namespace Systems
{
    public abstract class System : MonoBehaviour
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

        public virtual bool TrySpend(int value)
        {
            if (CurrentCount >= value)
            {
                CurrentCount -= value;
                return true;
            }

            return false;
        }

        public virtual bool TryRefill(int value)
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