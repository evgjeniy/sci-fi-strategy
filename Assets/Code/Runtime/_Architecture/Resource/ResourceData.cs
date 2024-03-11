using System;
using UnityEngine;

namespace SustainTheStrain._Architecture.Resource
{
    public class ResourceData : ScriptableObject, IModel<ResourceData>
    {
        [SerializeField, Min(0)] private int _money;

        public int Money
        {
            get => _money;
            set
            {
                if (value < 0 || value == _money) return;
                _money = value;
                Changed?.Invoke(this);
            }
        }

        public event Action<ResourceData> Changed;
    }
}