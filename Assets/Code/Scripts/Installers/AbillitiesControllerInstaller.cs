using SustainTheStrain.AbilitiesScripts;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class AbillitiesControllerInstaller : MonoInstaller
    {
        [SerializeField] private AbilitiesController _controller;

        public override void InstallBindings()
        {
            Container.Bind<AbilitiesController>().FromInstance(_controller).AsSingle();
        }
    }
}