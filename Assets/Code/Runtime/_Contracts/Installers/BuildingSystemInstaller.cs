using SustainTheStrain._Contracts.BuildingSystem;
using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class BuildingSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBuildingFactory>().To<BuildingFactory>();
            Container.Bind<IBuildingCreateMenuFactory>().To<BuildingCreateMenuFactory>();
        }
    }
}