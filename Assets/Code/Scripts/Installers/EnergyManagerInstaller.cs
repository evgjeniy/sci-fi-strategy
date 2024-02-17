using SustainTheStrain.EnergySystem;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnergyManagerInstaller : MonoInstaller
    {
        [SerializeField] private EnergyManager _energyManager;
        public override void InstallBindings()
        {
            Container.Bind<EnergyManager>().FromInstance(_energyManager).AsSingle();
        }
    }
}