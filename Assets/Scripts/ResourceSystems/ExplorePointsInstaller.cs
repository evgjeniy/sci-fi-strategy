using UnityEngine;
using Zenject;

namespace ResourceSystems
{
    public class ExplorePointsInstaller : MonoInstaller
    {
        [SerializeField] private ResourceManager _resourceManager;

        public override void InstallBindings()
        {
            Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle();
            Container.QueueForInject(_resourceManager);
        }
    }
}