using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public interface IBuildingCreateMenuFactory
    {
        public BuildingCreateMenu Create(IPlaceholder placeholder);
    }
    
    public class BuildingCreateMenuFactory : IBuildingCreateMenuFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IBuildingFactory _buildingFactory;

        public BuildingCreateMenuFactory(IInstantiator instantiator, IBuildingFactory buildingFactory)
        {
            _instantiator = instantiator;
            _buildingFactory = buildingFactory;
        }

        public BuildingCreateMenu Create(IPlaceholder placeholder) =>
            _instantiator.InstantiatePrefabResourceForComponent<BuildingCreateMenu>(Const.ResourcePath.Buildings.Prefabs.BuildingCreateMenu, placeholder.transform)
                .With(x => x.CreateRocket.onClick.AddListener(() => _buildingFactory.Create<Rocket>(placeholder)))
                .With(x => x.CreateLaser.onClick.AddListener(() => _buildingFactory.Create<Laser>(placeholder)))
                .With(x => x.CreateArtillery.onClick.AddListener(() => _buildingFactory.Create<Artillery>(placeholder)))
                .With(x => x.CreateBarrack.onClick.AddListener(() => _buildingFactory.Create<Barrack>(placeholder)))
                .With(x => x.transform.LookAtCamera());
    }
}