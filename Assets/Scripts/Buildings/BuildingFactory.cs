using SustainTheStrain.Buildings.Components;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BuildingFactory : IFactory<Building>
    {
        private readonly DiContainer _diContainer;
        private readonly IBuildingSystem _buildingSystem;

        public BuildingFactory(DiContainer diContainer, IBuildingSystem buildingSystem)
        {
            _diContainer = diContainer;
            _buildingSystem = buildingSystem;
        }

        public Building Create() => _diContainer.InstantiatePrefabForComponent<Building>(_buildingSystem.SelectedData.Prefab);
    }
}