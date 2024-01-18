using SustainTheStrain.Input;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class InputServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().FromNew().AsSingle();
        }
    }
}