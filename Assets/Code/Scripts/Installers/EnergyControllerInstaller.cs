using SustainTheStrain.EnergySystem;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnergyControllerInstaller : MonoInstaller
    {
        [SerializeField] private EnergyController _controller;

        public override void InstallBindings()
        {
            Container.Bind<EnergyController>().FromInstance(_controller).AsSingle();
        }
    }
}