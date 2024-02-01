using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.EnergySystem
{
    public class EnergyController : MonoBehaviour
    {
        [Inject] public EnergyManager Manager { get; private set; }
        public event Action<IEnergySystem> OnSystemAdded;
        public List<IEnergySystem> Systems => _systems;
        private List<IEnergySystem> _systems = new();

        [Inject] public void AddEnergySystem(IEnergySystem system)
        {
            if (_systems.Contains(system)) return;
            _systems.Add(system);
            OnSystemAdded?.Invoke(system);
        }

        public bool TryGetEnergy(int countOfEnergy)
        {
            return Manager.TrySpend(countOfEnergy);
        }
    
        public void IncreaseMaxEnergy(int value)
        {
            Manager.IncreaseMaxEnergy(value);
        }
    
        public bool TryReturnEnergy(int countOfEnergy)
        {
            return Manager.TryRefill(countOfEnergy);
        }
    
    }
}
