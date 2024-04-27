using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;

namespace SustainTheStrain.Abilities
{
    public interface IAbility : IInputSelectable, IEnergySystem
    {
        public IObservable<ITimer> CooldownTimer { get; }
    }
}