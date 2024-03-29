using SustainTheStrain.Buildings;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class BuildingSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
        }
    }
}