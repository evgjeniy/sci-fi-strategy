using SustainTheStrain._Contracts.BuildingSystem;
using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class BuildingSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<IPlaceholder, BuildingCreateMenu, BuildingCreateMenu.Factory>()
                .FromFactory<BuildingCreateMenuFactory>();

            Container
                .BindFactory<BuildingType, IPlaceholder, Building, Building.Factory>()
                .FromFactory<BuildingFactory>();
        }
    }
}