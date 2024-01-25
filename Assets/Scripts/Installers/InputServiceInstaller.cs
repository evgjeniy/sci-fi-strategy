using SustainTheStrain.Input;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class InputServiceInstaller : MonoInstaller
    {
        [SerializeField] private InputService.Settings _settings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputService>().AsSingle().WithArguments(_settings);
        }
    }
}