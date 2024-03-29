using SustainTheStrain.Abilities;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.UI;
using SustainTheStrain.Input.UI;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain
{
    public class AbilityUIController
    {
        private EnergyController _energyController;
        private AbilitiesUIController _abilitiesUIController;

        public AbilityUIController(EnergyController energyCntroller, AbilitiesUIController abilitiesUIController)
        {
            _energyController = energyCntroller;
            _abilitiesUIController = abilitiesUIController;
        }
        
        public void MakeSubscriptions(EnergySystemUI ui, IEnergySystem system, Slider slider)
        {
            var button = ui.ControllButton;
            _abilitiesUIController.AddControlButton(button.GetComponent<InputSystemButtonBridge>(), slider);
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