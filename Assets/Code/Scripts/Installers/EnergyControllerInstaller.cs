﻿using System.Collections.Generic;
using SustainTheStrain.Abilities;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.UI;
using SustainTheStrain.EnergySystem.UI.Factories;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class EnergyControllerInstaller : MonoInstaller
    {
        [SerializeField] private EnergyController _controller;
        [SerializeField] private List<EnergySystemUISettings> _settings;

        public override void InstallBindings()
        {
            Container.Bind<EnergyController>().FromInstance(_controller).AsSingle();
            Container.Bind<EnergySystemUIFactoryManager>()
                .FromInstance(new EnergySystemUIFactoryManager(BuildFactories())).AsSingle();
        }
        
        private Dictionary<EnergySystemUIType, IFactory<IEnergySystem, EnergySystemUI>> BuildFactories()
        {
            var factories = new Dictionary<EnergySystemUIType, IFactory<IEnergySystem, EnergySystemUI>>();
            foreach (var setting in _settings)
            {
                var type = setting.UIType;
                if (!factories.ContainsKey(type))
                {
                    factories.Add(type, GetFactoryByType(setting));
                }
            }

            return factories;
        }

        private IFactory<IEnergySystem, EnergySystemUI> GetFactoryByType(EnergySystemUISettings settings)
        {
            switch (settings.UIType)
            {
                case EnergySystemUIType.Generator:
                    return new ResourceGeneratorUIFactory(settings, new ResourceGeneratorUIController(_controller));
                case EnergySystemUIType.Ability:
                    return new AbilityUIFactory(settings, new AbilityUIController(_controller, new AbilitiesUIController()));
                default:
                    return new BasicEnergySystemUIFactory(settings, new BaseEnergyUIController(_controller));
            }
        }
    }
}