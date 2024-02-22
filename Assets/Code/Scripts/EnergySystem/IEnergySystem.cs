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
        public int FreeEnergyCells { get; }
        public int MaxEnergy { get; }
        public int CurrentEnergy { get; set; }
        public event Action<int> OnCurrentEnergyChanged;
        public event Action<int> OnMaxEnergyChanged;
        public event Action<IEnergySystem> OnEnergyAddRequire;
        public event Action<IEnergySystem> OnEnergyDeleteRequire;
        
        public void IncreaseMaxEnergy(int value){}
        
        public void TrySpendEnergy() {}

        public void TryRefillEnergy() {}

        public void SetEnergySettings(EnergySystemSettings settings) { }
    }
}