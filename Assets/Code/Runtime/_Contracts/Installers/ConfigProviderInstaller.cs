using SustainTheStrain._Contracts.Configs;
using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class ConfigProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IConfigProviderService>().To<ConfigProviderService>().AsSingle();
        }
    }
}