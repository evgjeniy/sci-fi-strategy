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
                if (value > _maxCount)
                {
                    _currentCount = _maxCount;
                }
                else
                {
                    if (value < 0) return;
                    _currentCount = value;
                    
                }
                OnValueChanged?.Invoke(_currentCount);
            }
        }

        //public int FreeCount => _maxCount - _currentCount;
        public Action<int> OnValueChanged;

        private void OnEnable()
        {
            CurrentCount = _maxCount;
        }

        public virtual void Spend(int value)
        {
            if (CurrentCount >= value)
            {
                CurrentCount -= value;
            }
        }
        
    }
}