using SustainTheStrain.Abilities;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.EnergySystem.UI;
using SustainTheStrain.Input;
using SustainTheStrain.Input.UI;
using UnityEngine.UI;

namespace SustainTheStrain
{
    public class AbilityUIController
    {
        private readonly EnergyController _energyController;
        private readonly IInputSystem _inputSystem;

        public AbilityUIController(EnergyController energyCntroller, IInputSystem inputSystem)
        {
            _energyController = energyCntroller;
            _inputSystem = inputSystem;
        }
        
        public void MakeSubscriptions(EnergySystemUI ui, Abilities.New.IAbility ability, Slider slider)
        {
            ui.ControllButton.OnLeftMouseClick += () =>
            {
                if (ability.CurrentEnergy == 0)
                    _energyController.TryLoadEnergyToSystem(ability);
                else
                    _inputSystem.Select(ability);
            };
            ui.ControllButton.OnRightMouseClick += () =>
            {
                if (ability.CurrentEnergy == 0) return;
                
                _energyController.TryReturnEnergyFromSystem(ability);
                _inputSystem.Idle();
            };
            ability.CooldownTimer.Changed += timer => slider.value = timer.Percent;
            ability.Changed += ui.ChangeEnergy;
        }
    }
}