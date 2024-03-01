using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class EnergySystemUIFactoryManager : MonoBehaviour
    {
        [SerializeField] private List<SystemFactoryPair> Factories;

        public EnergySystemUI Create(IEnergySystem system)
        {
            return Find(system)?.Create(system);
        }

        private IFactory<IEnergySystem, EnergySystemUI> Find(IEnergySystem system)
        {
            return (from factory in Factories where factory.systemUIType == system.EnergySettings.SystemUIType select factory.Factory).FirstOrDefault();
        }
        
        [Serializable]
        public struct SystemFactoryPair
        {
            public EnergySystemUIType systemUIType;
            public MonoUIFactory Factory;
        }
    }
}