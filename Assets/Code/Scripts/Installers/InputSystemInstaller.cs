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
        }
    }
}