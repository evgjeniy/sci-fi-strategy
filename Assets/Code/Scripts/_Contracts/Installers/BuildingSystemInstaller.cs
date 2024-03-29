using SustainTheStrain._Contracts.Buildings;
using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class BuildingSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
        }
    }
}