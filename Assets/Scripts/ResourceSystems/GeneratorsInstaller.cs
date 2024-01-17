using UnityEngine;
using Zenject;

namespace ResourceSystems
{
    public class GeneratorsInstaller : MonoInstaller
    {
        [SerializeField] private GoldGenerator _goldGenerator;
        [SerializeField] private ExplorePointGenerator _explorePointGenerator;

        public override void InstallBindings()
        {
            Container.Bind<ExplorePointGenerator>().FromInstance(_explorePointGenerator).AsSingle();
            Container.Bind<GoldGenerator>().FromInstance(_goldGenerator).AsSingle();
            Container.QueueForInject(_explorePointGenerator);
            Container.QueueForInject(_goldGenerator);
        }
    }
}