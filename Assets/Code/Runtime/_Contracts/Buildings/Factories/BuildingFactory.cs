using System;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BuildingFactory : IFactory<BuildingType, Building>
    {
        private readonly IInstantiator _instantiator;

        public BuildingFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public Building Create(BuildingType type)
        {
            var path = type.GetCreateButtonPath();
            
            return type switch
            {
                BuildingType.Rocket => _instantiator.InstantiatePrefabResourceForComponent<Rocket>(path),
                BuildingType.Laser => _instantiator.InstantiatePrefabResourceForComponent<Laser>(path),
                BuildingType.Artillery => _instantiator.InstantiatePrefabResourceForComponent<Rocket>(path),
                BuildingType.Barrack => _instantiator.InstantiatePrefabResourceForComponent<Rocket>(path),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid building type")
            };
        }
    }
}