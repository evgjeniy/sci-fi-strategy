﻿using UnityEngine;

namespace SustainTheStrain
{
    public interface IObservable<out TValue>
    {
        public TValue Value { get; }
        event System.Action<TValue> Changed;
    }
    
    [System.Serializable]
    public class Observable<TValue> : IObservable<TValue>
    {
        [SerializeField] private TValue _value;

        private event System.Action<TValue> OnChangedEvent = _ => {};

        public Observable(TValue value = default) => _value = value;

        public virtual TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChangedEvent(_value);
            }
        }
    
        public event System.Action<TValue> Changed
        {
            add
            {
                OnChangedEvent += value;
                value(_value);
            }
            remove => OnChangedEvent -= value;
        }

        public static implicit operator TValue(Observable<TValue> observable) => observable.Value;
    }
}