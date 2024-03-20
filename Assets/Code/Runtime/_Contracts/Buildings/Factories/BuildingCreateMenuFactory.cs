using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuildingCreateMenuFactory
    {
        public BuildingCreateMenu Create(IPlaceholder placeholder);
    }
    
    public class BuildingCreateMenuFactory : IBuildingCreateMenuFactory
    {
        private readonly IInstantiator _instantiator;

        public BuildingCreateMenuFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public BuildingCreateMenu Create(IPlaceholder placeholder) =>
            _instantiator.InstantiatePrefabResourceForComponent<BuildingCreateMenu>
            (
                Const.ResourcePath.Buildings.Prefabs.BuildingCreateMenu,
                new[] { placeholder }
            );
    }
}