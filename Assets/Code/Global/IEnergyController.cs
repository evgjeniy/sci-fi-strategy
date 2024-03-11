using System.Collections.Generic;
using SustainTheStrain.Utils;

namespace SustainTheStrain.Global
{
    public interface IEnergyController : IController<IEnergyControllerData, IEnergyControllerView>
    {
        public bool TryAddSystem(IEnergySystem system);
        public bool TryRemoveSystem(IEnergySystem system);
    }
    
    public class EnergyController : IEnergyController
    {
        public IEnergyControllerData Model { get; }
        public IEnergyControllerView View { get; }

        public EnergyController(IEnergyControllerData model, IEnergyControllerView view)
        {
            Model = model;
            View = view;
        }

        public bool TryAddSystem(IEnergySystem system)
        {
            Model.EnergySystems.Add(system);
            return true;
        }

        public bool TryRemoveSystem(IEnergySystem system) => Model.EnergySystems.Remove(system);
    }

    public interface IEnergyControllerData
    {
        public List<IEnergySystem> EnergySystems { get; }
        public IObservable<IEnergyProvider> EnergyProvider { get; }
    }
    
    public interface IEnergyControllerView
    {
        public void DisplayEnergyCells(IEnergyProvider provider);
    }

    public interface IEnergyProvider : IObservable<IEnergyProvider>
    {
        public Observable<int> MaxEnergy { get; }
        public Observable<int> CurrentEnergy { get; }

        public bool TryGetEnergy(int count);
        public bool TryReturnEnergy(int count);
    }

    public interface IEnergySystem
    {
        public IEnergyProvider EnergyProvider { get; }
    }

    public interface IAbilitySystem : IEnergySystem {}
    public interface IBuildingSystem : IEnergySystem {}
    public interface IShieldSystem : IEnergySystem {}
    public interface IMineSystem : IEnergySystem {}
    public interface IHeroSystem : IEnergySystem {}
}