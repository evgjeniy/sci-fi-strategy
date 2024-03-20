using SustainTheStrain._Contracts.Buildings;
using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class BuildingSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
            Container.Bind<IBuildingCreateMenuFactory>().To<BuildingCreateMenuFactory>().AsSingle();
            Container.Bind<IBuildingViewFactory>().To<BuildingViewFactory>().AsSingle();
        }
    }
}