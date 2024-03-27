using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class EnergySystemUIFactoryManager 
    {
        private Dictionary<EnergySystemUIType, IFactory<IEnergySystem, EnergySystemUI>> _factories;

        public EnergySystemUIFactoryManager(Dictionary<EnergySystemUIType, IFactory<IEnergySystem, EnergySystemUI>> factories)
        {
            _factories = factories;
        }

        public EnergySystemUI Create(IEnergySystem system)
        {
            return _factories.ContainsKey(system.EnergySettings.SystemUIType) ? _factories[system.EnergySettings.SystemUIType].Create(system) : null;
        }

        /*private IFactory<IEnergySystem, EnergySystemUI> Find(IEnergySystem system)
        {
            return (from factory in Factories where factory.systemUIType == system.EnergySettings.SystemUIType select factory.Factory).FirstOrDefault();
        }*/
        
    }
}