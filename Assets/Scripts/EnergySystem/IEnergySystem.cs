using System;
using UnityEngine;
using Zenject;

namespace Systems
{
    public interface IEnergySystem
    {
        [Inject] public EnergyController EnergyController { get; set; }
        public int EnergySpendCount { get; }
        public int FreeEnergyCells { get; }

        public int MaxEnergy { get; }
        public int CurrentEnergy { get; }
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        
        public void IncreaseMaxEnergy(int value){}
        
        public void TrySpendEnergy() {}

        public void TryRefillEnergy() {}
    }
}