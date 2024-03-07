using SustainTheStrain.Citadels;
using SustainTheStrain.Level;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class GameCycleInstaller : MonoInstaller
    {
        [SerializeField] private GameModeController _gameModeController;
        [SerializeField] private WavesManager _wavesManager;
        [SerializeField] private Citadel _citadel;

        public override void InstallBindings()
        {
            Container.Bind<WavesManager>().FromInstance(_wavesManager).AsSingle();
            Container.Bind<Citadel>().FromInstance(_citadel).AsSingle();
            Container.Bind<GameModeController>().FromInstance(_gameModeController).AsSingle();
        }
    }
}