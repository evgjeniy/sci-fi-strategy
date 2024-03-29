﻿using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.UI;
using SustainTheStrain.ResourceSystems;
using SustainTheStrain.ResourceSystems.UI;
using Zenject;

namespace SustainTheStrain
{
    public class ResourceGeneratorUIController
    {
        
        private EnergyController _energyController;

        public ResourceGeneratorUIController(EnergyController controller)
        {
            _energyController = controller;
        }
        
        public void MakeSubscriptions(EnergySystemUI ui, IEnergySystem system)
        {
            var button = ui.ControllButton;
            var generatorUI = new GeneratorUI(button.transform, system as ResourceGenerator);
            button.OnLeftMouseClick += () =>
            {
                _energyController.TryLoadEnergyToSystem(system);
            };
            button.OnRightMouseClick += () =>
            {
                _energyController.TryReturnEnergyFromSystem(system);
            };
            system.Changed += ui.ChangeEnergy;
        }
    }
}