using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public class BuildingCreateMenuFactory : IFactory<IPlaceholder, BuildingCreateMenu>
    {
        private readonly DiContainer _container;
        private readonly Building.Factory _buildingFactory;

        public BuildingCreateMenuFactory(DiContainer container, Building.Factory buildingFactory)
        {
            _container = container;
            _buildingFactory = buildingFactory;
        }

        public BuildingCreateMenu Create(IPlaceholder placeholder) =>
            _container.InstantiatePrefabResourceForComponent<BuildingCreateMenu>(Const.ResourcePath.Buildings.Prefabs.BuildingCreateMenu, placeholder.transform)
                .With(x => x.CreateRocket.onClick.AddListener(() => _buildingFactory.Create(BuildingType.Rocket, placeholder)))
                .With(x => x.CreateLaser.onClick.AddListener(() => _buildingFactory.Create(BuildingType.Laser, placeholder)))
                .With(x => x.CreateArtillery.onClick.AddListener(() => _buildingFactory.Create(BuildingType.Artillery, placeholder)))
                .With(x => x.CreateBarrack.onClick.AddListener(() => _buildingFactory.Create(BuildingType.Barrack, placeholder)))
                .With(x => x.transform.LookAtCamera());
    }
}