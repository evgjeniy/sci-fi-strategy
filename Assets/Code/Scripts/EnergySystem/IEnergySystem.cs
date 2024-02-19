using System;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;

namespace SustainTheStrain.EnergySystem
{
    public interface IEnergySystem
    {
        public EnergySystemSettings EnergySettings { get; }
        public Sprite ButtonImage { get;}
        public EnergyController EnergyController { get; set; }
        public int EnergySpendCount { get; }
        public int FreeEnergyCells { get; }

        public int MaxEnergy { get; }
        public int CurrentEnergy { get; }
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        
        public void IncreaseMaxEnergy(int value){}
        
        public void TrySpendEnergy() {}

        public void TryRefillEnergy() {}

        public void SetEnergySettings(EnergySystemSettings settings) { }
    }
}