using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.EnergySystem
{
    public class EnergyController : MonoBehaviour
    {
    

        public List<IEnergySystem> Systems => _systems;
        private List<IEnergySystem> _systems = new();

        [Inject] public EnergyManager Manager { get; private set; }
    
        public void AddEnergySystem(IEnergySystem system)
        {
            if (!_systems.Contains(system))
            {
                _systems.Add(system);
            }
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
