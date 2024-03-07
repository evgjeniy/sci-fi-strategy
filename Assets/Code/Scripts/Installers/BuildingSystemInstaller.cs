using SustainTheStrain.Buildings;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class BuildingSystemInstaller : MonoInstaller
    {
        [SerializeField] private BuildingSystem _buildingSystemPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<BuildingData, Building, Building.Factory>().FromFactory<BuildingFactory>();
            Container.Bind<BuildingSystem>().FromComponentInNewPrefab(_buildingSystemPrefab).AsSingle().NonLazy();
        }
    }
}