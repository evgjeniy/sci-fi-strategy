using System.Collections.Generic;
using SustainTheStrain.Abilities;
using SustainTheStrain.EnergySystem.UI.Factories;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI
{
    public class EnergySystemsUIController : MonoBehaviour
    {
        [SerializeField] private AbilitiesUIController _abilitiesUIController;
        
        private EnergyController EnergyController { get; set; }
        private EnergySystemUIFactoryManager _uiFactory;
        
        [Inject] public void AddSystemsUIFactory(EnergySystemUIFactoryManager energySystemUIFactory)
        {
            _uiFactory = energySystemUIFactory;
        }
        
        [Inject] public void InitializeComponent(EnergyController controller)
        {
            EnergyController = controller;
            foreach (var system in EnergyController.Systems)
            {
                GenerateNewUI(system);
            }
            EnergyController.OnSystemAdded += GenerateNewUI;
        }

        private void GenerateNewUI(IEnergySystem system)
        {
            var ui = _uiFactory.Create(system);
            system.Changed += ui.ChangeEnergy;
        }
        
        private void OnDisable()
        {
            EnergyController.OnSystemAdded -= GenerateNewUI;
        }
    }
}