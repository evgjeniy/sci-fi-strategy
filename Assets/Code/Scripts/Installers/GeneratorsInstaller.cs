using SustainTheStrain.EnergySystem;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class GeneratorsInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_goldGenerator")] [SerializeField] private Mine mine;
        // [SerializeField] private ExplorePointGenerator _explorePointGenerator;

        public override void InstallBindings()
        {
            mine.LoadSettings();
            Container.Bind<Mine>().FromInstance(mine).AsSingle();

            Container.Bind<IEnergySystem>().FromInstance(mine);
            // Container.Bind<ExplorePointGenerator>().FromInstance(_explorePointGenerator).AsSingle();
        }
    }
}