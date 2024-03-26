using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class ResourceManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IResourceManager>().To<ResourceManager>().AsSingle();
        }
    }

    public interface IResourceManager
    {
        public Observable<int> Gold { get; }
        public bool TrySpend(int spendValue);
    }

    public class ResourceManager : IResourceManager
    {
        public Observable<int> Gold { get; } = new(1000);
        
        public bool TrySpend(int spendValue)
        {
            var goldAfterSpend = Gold.Value - spendValue;
            if (goldAfterSpend < 0) return false;

            Gold.Value = goldAfterSpend;
            return true;
        }
    }
}