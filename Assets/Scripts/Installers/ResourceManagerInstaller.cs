using SustainTheStrain.ResourceSystems;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class ResourceManagerInstaller : MonoInstaller
    {
        [SerializeField] private ResourceManager _resourceManager;

        public override void InstallBindings()
        {
            Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle();
        }
    }
}