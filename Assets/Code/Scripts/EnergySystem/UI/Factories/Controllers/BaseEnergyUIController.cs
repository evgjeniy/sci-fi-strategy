using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.UI;
using UnityEngine;
using Zenject;

namespace SustainTheStrain
{
    public class BaseEnergyUIController
    {
        [Inject] private static EnergyController _energyController;

        public BaseEnergyUIController(EnergyController controller)
        {
            _energyController = controller;
        }
        
        public void MakeSubscriptions(EnergySystemUI ui, IEnergySystem system)
        {
            var button = ui.ControllButton;
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
