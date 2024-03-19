using Zenject;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public interface IBuildingFactory
    {
        public TBuilding Create<TBuilding>(IPlaceholder placeholder) where TBuilding : Building;
    }
    
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IInstantiator _instantiator;

        public BuildingFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public TBuilding Create<TBuilding>(IPlaceholder placeholder) where TBuilding : Building
        {
            var path = Const.ResourcePath.Buildings.Prefabs.Root + $"/{typeof(TBuilding).Name}";
            var building = _instantiator.InstantiatePrefabResourceForComponent<TBuilding>(path);
            
            placeholder.SetBuilding(building);
            return building;
        }
    }
}