using System;
using System.Collections.Generic;
using System.Linq;

namespace SustainTheStrain.Abilities.New
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

        public AbilityController(Zenject.IInstantiator instantiator)
        {
            _abilitiesList = new List<IAbility>(capacity: 4)
            {
                instantiator.Instantiate<ZoneDamageAbility>(),
                instantiator.Instantiate<ZoneSlownessAbility>(),
                instantiator.Instantiate<LandingAbility>()
            };
            _abilitiesDictionary = _abilitiesList.ToDictionary(ability => ability.GetType());
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