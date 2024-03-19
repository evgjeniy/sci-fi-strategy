using System;
using Zenject;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public class BuildingFactory : IFactory<BuildingType, IPlaceholder, Building>
    {
        private readonly IInstantiator _instantiator;

        public BuildingFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public Building Create(BuildingType type, IPlaceholder placeholder)
        {
            var path = type.GetPrefabPath();

            Building building = type switch
            {
                BuildingType.Rocket => _instantiator.InstantiatePrefabResourceForComponent<Rocket>(path),
                BuildingType.Laser => _instantiator.InstantiatePrefabResourceForComponent<Laser>(path),
                BuildingType.Artillery => _instantiator.InstantiatePrefabResourceForComponent<Artillery>(path),
                BuildingType.Barrack => _instantiator.InstantiatePrefabResourceForComponent<Barrack>(path),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid building type")
            };
            
            placeholder.SetBuilding(building);
            
            return building;
        }
    }
}