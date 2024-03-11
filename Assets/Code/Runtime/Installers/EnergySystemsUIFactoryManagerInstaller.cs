using SustainTheStrain.EnergySystem.UI;
using SustainTheStrain.EnergySystem.UI.Factories;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnergySystemsUIFactoryManagerInstaller : MonoInstaller
    {
        [SerializeField] private EnergySystemUIFactoryManager _manager;

        public override void InstallBindings()
        {
            Container.Bind<EnergySystemUIFactoryManager>().FromInstance(_manager).AsSingle();
        }
    }
}