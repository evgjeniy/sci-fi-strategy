using System;
using SustainTheStrain._Architecture;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;

namespace SustainTheStrain.EnergySystem
{
    public interface IEnergySystem : IModel<IEnergySystem>
    {
        public EnergySystemSettings EnergySettings { get; }
        public int FreeEnergyCellsCount { get; }
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set; }
        public bool TrySpendEnergy();
        public bool TryRefillEnergy();
    }
}