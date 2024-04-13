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

        public delegate bool CheckGoldDelegate(int x);
        private CheckGoldDelegate _checkGoldAction;
        
        [Inject]
        public void AddEnergySystem(IEnergySystem system)
        {
            if (_systems.Contains(system)) return;
            _systems.Add(system);
            OnSystemAdded?.Invoke(system);
        }
        
        public void TryLoadEnergyToSystem(IEnergySystem system)
        {
            var countOfEnergy = system.EnergySettings.EnergySpend;
            if (system.FreeEnergyCellsCount < countOfEnergy) return;
            if (Manager.TrySpend(countOfEnergy))
            {
                system.TrySpendEnergy(countOfEnergy);
            }
        }
        public void TryReturnEnergyFromSystem(IEnergySystem system)
        {
            var countOfEnergy = system.EnergySettings.EnergySpend;
            if (system.CurrentEnergy < countOfEnergy) return;
            if (Manager.TryRefill(countOfEnergy))
            {
                system.TryRefillEnergy(countOfEnergy);
            }
        }

        public void SetCheckGoldAction( CheckGoldDelegate action)
        {
            _checkGoldAction = action;
        }

        public void BuyMaxEnergy()
        {
            if (!_checkGoldAction.Invoke(Manager.UpgradeCost)) return;
            Manager.UpgradeEnergyCount();
        }
        
        
    }
}
