using SustainTheStrain.Abilities;
using SustainTheStrain.Input.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class AbilityUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        [Inject] private EnergyController _energyController;
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        [Inject] private AbilitiesUIController mAbilitiesUIController;

        public AbilityUIFactory(EnergySystemUISettings settings)
        {
            _uiPrefab = settings.UIPrefab;
            _spawnParent = settings.SpawnParent;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Object.Instantiate(_uiPrefab, _spawnParent);
            var button = ui.ControllButton;
            var slider = button.transform.parent.GetComponent<Slider>();
            slider.value = 0;
            mAbilitiesUIController.AddControlButton(button.GetComponent<InputSystemButtonBridge>(), slider);
            button.image.sprite = system.EnergySettings.ButtonImage;
            ui.MaxBarsCount = system.EnergySettings.MaxEnergy;
            button.OnLeftMouseClick += () =>
            {
                _energyController.TryLoadEnergyToSystem(system);
            };
            button.OnRightMouseClick += () =>
            {
                _energyController.TryReturnEnergyFromSystem(system);
            };
            return ui;
        }
    }
}