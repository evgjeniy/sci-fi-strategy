using SustainTheStrain.EnergySystem;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class GeneratorsInstaller : MonoInstaller
    {
        [SerializeField] private GoldGenerator _goldGenerator;
        [SerializeField] private ExplorePointGenerator _explorePointGenerator;

        public override void InstallBindings()
        {
            Container.Bind<ExplorePointGenerator>().FromInstance(_explorePointGenerator).AsSingle();
            Container.Bind<GoldGenerator>().FromInstance(_goldGenerator).AsSingle();
        }
    }
}