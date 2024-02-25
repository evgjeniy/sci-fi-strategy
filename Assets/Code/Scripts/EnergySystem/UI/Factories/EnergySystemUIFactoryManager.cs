using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class EnergySystemUIFactoryManager : MonoBehaviour
    {
        [SerializeField] private List<SystemFactoryPair> Factories;

        public EnergySystemUI Create(IEnergySystem system)
        {
            return Find(system)?.Create(system);
        }

        private IEnergySystemUIFactory Find(IEnergySystem system)
        {
            return (from factory in Factories where factory.SystemType == system.EnergySettings.SystemType select factory.Factory).FirstOrDefault();
        }
        
        [Serializable]
        public struct SystemFactoryPair
        {
            public EnergySystemType SystemType;
            public MonoUIFactory Factory;
        }
    }
}