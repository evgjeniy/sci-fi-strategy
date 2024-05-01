using System;
using System.Collections.Generic;
using System.Linq;
using SustainTheStrain.EnergySystem;

namespace SustainTheStrain.Abilities
{
    public interface IAbilityController
    {
        IReadOnlyList<IAbility> Abilities { get; }
        bool TryGetAbility<TAbility>(out TAbility ability) where TAbility : IAbility;
    }

    public class AbilityController : IAbilityController
    {
        private readonly Dictionary<Type, IAbility> _abilitiesDictionary;
        private readonly List<IAbility> _abilitiesList;

        public IReadOnlyList<IAbility> Abilities => _abilitiesList;

        public AbilityController(Zenject.IInstantiator instantiator, EnergyController energyController)
        {
            _abilitiesList = new List<IAbility>(capacity: 4)
            {
                instantiator.Instantiate<ZoneDamageAbility>(),
                instantiator.Instantiate<ZoneSlownessAbility>(),
                instantiator.Instantiate<ChainDamageAbility>(),
                instantiator.Instantiate<LandingAbility>(),
                instantiator.Instantiate<FreezeAbility>()
            };
            _abilitiesDictionary = _abilitiesList.ToDictionary(ability => ability.GetType());

            foreach (var ability in _abilitiesList)
                energyController.AddEnergySystem(ability);
        }

        public bool TryGetAbility<TAbility>(out TAbility ability) where TAbility : IAbility
        {
            ability = default;
            if (!_abilitiesDictionary.TryGetValue(typeof(TAbility), out var baseAbility)) return false;
            if (baseAbility is not TAbility concreteAbility) return false;

            ability = concreteAbility;
            return true;
        }
    }
}