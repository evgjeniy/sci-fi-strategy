using SustainTheStrain.Buildings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class BuildingSystemInstaller : MonoInstaller
    {
        [SerializeField] private BuildingSystem _buildingSystemPrefab;
    
        public override void InstallBindings()
        {
            Container.Bind<BuildingSystem>().FromComponentInNewPrefab(_buildingSystemPrefab).AsSingle();
        }
    }
}
