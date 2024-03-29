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
            Container.BindInterfacesAndSelfTo<ResourceManager>().FromInstance(_resourceManager).AsSingle();
        }
    }
}