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

        [Inject] 
        public void AddEnergySystem(IEnergySystem system)
        {
            if (_systems.Contains(system)) return;
            _systems.Add(system);
            system.OnEnergyAddRequire+=TryLoadEnergyToSystem;
            system.OnEnergyDeleteRequire += TryReturnEnergyFromSystem;
            OnSystemAdded?.Invoke(system);
        }

        private void TryLoadEnergyToSystem(IEnergySystem system)
        {
            var countOfEnergy = system.EnergySettings.EnergySpend;
            if (system.FreeEnergyCells<countOfEnergy) return;
            if (Manager.TrySpend(countOfEnergy))
            {
                system.CurrentEnergy += countOfEnergy;
            }
        }
    
        public void IncreaseMaxEnergy(int value)
        {
            Manager.IncreaseMaxEnergy(value);
        }
    
        private void TryReturnEnergyFromSystem(IEnergySystem system)
        {
            var countOfEnergy = system.EnergySettings.EnergySpend;
            if (system.CurrentEnergy < countOfEnergy) return;
            if (Manager.TryRefill(countOfEnergy))
            {
                system.CurrentEnergy -= countOfEnergy;
            }
        }
    
    }
}
