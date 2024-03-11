using System;

namespace SustainTheStrain.Utils
{
    public interface IObservable<out TValue>
    {
        public TValue Value { get; }
        public event Action<TValue> Changed;
    }
}