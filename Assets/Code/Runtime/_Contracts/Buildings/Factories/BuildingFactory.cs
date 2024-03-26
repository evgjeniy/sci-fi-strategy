using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuildingFactory
    {
        public TBuilding Create<TBuilding>(IPlaceholder placeholder) where TBuilding : IBuilding;
    }
    
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IInstantiator _instantiator;

        public BuildingFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public TBuilding Create<TBuilding>(IPlaceholder placeholder) where TBuilding : IBuilding
        {
            var building = _instantiator.InstantiatePrefabResourceForComponent<TBuilding>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/{typeof(TBuilding).Name}",
                extraArgs: new[] { placeholder }
            );
            
            placeholder.SetBuilding(building);
            return building;
        }
    }
}