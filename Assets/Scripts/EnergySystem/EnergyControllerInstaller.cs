using System;
using UnityEngine;
using Zenject;

namespace Systems
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