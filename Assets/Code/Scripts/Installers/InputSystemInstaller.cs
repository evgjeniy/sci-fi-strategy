using SustainTheStrain.Input;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class InputSystemInstaller : MonoInstaller
    {
        [SerializeField] private InputData _data;

        public override void InstallBindings()
        {
            Container.BindInstance(new InputActions());
            Container.BindInterfacesTo<InputSystem>().AsSingle().WithArguments(_data);
            Container.BindInterfacesTo<InputService>().AsSingle(); // TODO: Remove after ability refactoring (OLD)
        }
    }
}