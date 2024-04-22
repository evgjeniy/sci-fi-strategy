using SustainTheStrain.Abilities;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class AbilitiesControllerInstaller : MonoInstaller
    {
        /*[SerializeField] private AbilitiesController _controller;*/

        public override void InstallBindings()
        {
            /*Container.Bind<AbilitiesController>().FromInstance(_controller).AsSingle();*/

            Container
                .Bind<Abilities.New.IAbilityController>()
                .To<Abilities.New.AbilityController>()
                .AsSingle();
        }
    }
}