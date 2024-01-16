using System;
using UnityEngine;
using Zenject;

namespace Systems
{
    public interface IEnergySystem
    {
        public EnergyManager _energyManager { get;}
        public int _energySpendCount { get;}
        public int MaxEnergy { get; }
        public Action<int> OnValueChanged { get; }
        public Action<int> OnMaxValueChanged { get; }
        [field: SerializeField]
        int CurrentEnergy { get; }

        public void IncreaseMaxEnergy(int value){}
        
        public void BindManager(EnergyManager manager)
        {
            //Dependency injection block
        }

        public void TrySpend()
        {
            //Here should be system upgrade logic
        }

        public void TryRefill()
        {
            //Here should be system downgrade logic
        }
    }
}