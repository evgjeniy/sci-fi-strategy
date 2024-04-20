using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;

namespace SustainTheStrain.Abilities.New
{
    public interface IAbility : IInputSelectable, IEnergySystem
    {
        public IObservable<ITimer> CooldownTimer { get; }
    }
}