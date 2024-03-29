using SustainTheStrain._Contracts;
using SustainTheStrain.Scriptable.EnergySettings;

namespace SustainTheStrain.EnergySystem
{
    public interface IEnergySystem : IObservable<IEnergySystem>
    {
        public EnergySystemSettings EnergySettings { get; }
        public int FreeEnergyCellsCount { get; }
        public int MaxEnergy { get; }
        public int CurrentEnergy { get; set; }
        public bool TrySpendEnergy(int count);
        public bool TryRefillEnergy(int count);
    }
}