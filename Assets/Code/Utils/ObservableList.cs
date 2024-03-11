using System;
using System.Collections.Generic;

namespace SustainTheStrain.Utils
{
    public class ObservableList<TValue> : IObservable<List<TValue>>
    {
        public List<TValue> Value { get; }
        public event Action<List<TValue>> Changed;

        public ObservableList() => Value = new List<TValue>();
        public ObservableList(List<TValue> value) => Value = value;
        
        
    }
}