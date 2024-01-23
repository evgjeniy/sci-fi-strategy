using SustainTheStrain.EnergySystem;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnergyControllerInstaller : MonoInstaller
    {
        [SerializeField] private EnergyController _controller;
        [SerializeField] private GoldGenerator _goldGenerator;
        [SerializeField] private ExplorePointGenerator _explorePointGenerator;

        public override void InstallBindings()
        {
            _controller.AddEnergySystem(_goldGenerator);
            _controller.AddEnergySystem(_explorePointGenerator);

            Container.Bind<EnergyController>().FromInstance(_controller).AsSingle();
        }
    }
}