using System;
using System.Collections.Generic;
using SustainTheStrain._Architecture;
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
            OnSystemAdded?.Invoke(system);
        }

        //используется в ui
        public void TryLoadEnergyToSystem(IEnergySystem system)
        {
            var countOfEnergy = system.EnergySettings.EnergySpend;
            if (system.FreeEnergyCellsCount < countOfEnergy) return;
            if (Manager.TrySpend(countOfEnergy))
            {
                system.CurrentEnergy += countOfEnergy;
            }
        }

        //используется в ui
        public void TryReturnEnergyFromSystem(IEnergySystem system)
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
