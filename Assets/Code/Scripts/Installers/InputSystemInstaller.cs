using SustainTheStrain.Input;
using UnityEngine;
using Zenject;

namespace SustainTheStrain._Contracts.Installers
{
    public class InputSystemInstaller : MonoInstaller
    {
        [SerializeField] private InputSettings _settings;

        public override void InstallBindings() => Container
            .BindInterfacesTo<InputSystem>()
            .AsSingle()
            .WithArguments(_settings);
    }
}