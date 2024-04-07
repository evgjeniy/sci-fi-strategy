using System;
using SustainTheStrain.Scriptable.EnergySettings;

namespace SustainTheStrain.EnergySystem
{
    public interface IEnergySystem
    {
        public EnergySystemSettings EnergySettings { get; }
        public event Action<IEnergySystem> Changed;
        public int FreeEnergyCellsCount => MaxEnergy - CurrentEnergy;
        public int MaxEnergy { get; }

        public int CurrentEnergy { get; set; }

        public bool TrySpendEnergy(int count)
        {
            var energyAfterSpend = CurrentEnergy + count;
            if (energyAfterSpend > MaxEnergy) return false;

            CurrentEnergy = energyAfterSpend;
            return true;
        }

        public bool TryRefillEnergy(int count)
        {
            var energyAfterRefill = CurrentEnergy - count;
            if (energyAfterRefill < 0) return false;

            CurrentEnergy = energyAfterRefill;
            return true;
        }
    }
}