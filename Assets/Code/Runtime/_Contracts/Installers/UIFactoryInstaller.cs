using SustainTheStrain._Contracts.Buildings;
using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class UIFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings() => Container
            .BindFactory<IPlaceholder, BuildingCreateMenu, BuildingCreateMenu.Factory>()
            .FromFactory<BuildingUICreateFactory>();
    }
}