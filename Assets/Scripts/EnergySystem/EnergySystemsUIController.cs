using System;
using System.Collections.Generic;
using SustainTheStrain.ResourceSystems;
using UnityEngine;

namespace SustainTheStrain.EnergySystem
{
    public class EnergySystemsUIController : MonoBehaviour
    {
        [SerializeField] private Dictionary<IEnergySystem, EnergySystemUI> _systemUIs = new();
        
        [Header("test")] 
        [SerializeField] private GoldGenerator system;
        [SerializeField] private EnergySystemUI UI;
        
        
        private void OnEnable()
        {
            // foreach (var system in _systemUIs)
            // {
            //     system.Value.BarsCount = system.Key.MaxEnergy;
            // }

            UI.BarsCount = system.MaxEnergy;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                UI.AddEnergy(1);
            }

            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                UI.DeleteEnergy(1);
            }
        }
    }
}