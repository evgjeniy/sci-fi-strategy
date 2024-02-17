using SustainTheStrain.EnergySystem.UI;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnergySystemsUIFactoryInstaller : MonoInstaller
    {
        [SerializeField] private EnergySystemUIFactory _factory;

        public override void InstallBindings()
        {
            Container.Bind<EnergySystemUIFactory>().FromInstance(_factory).AsSingle();
        }
    }
}