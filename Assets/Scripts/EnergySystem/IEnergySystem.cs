using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem
{
    public interface IEnergySystem
    {
        public Sprite ButtonImage { get;}
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