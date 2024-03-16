using SustainTheStrain.Abilities;
using SustainTheStrain.Input.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class AbilityUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        private EnergyController _energyController;
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        private Image _backgroundImage;
        private EnergySystemControllButton _controllButton;
        private AbilitiesUIController mAbilitiesUIController;
        private float _scaleMultiplayer;
        private Slider _sliderPrefab;
        
        public AbilityUIFactory(EnergyController controller, EnergySystemUI uiPrefab, Transform spawnParent,
            Image background, EnergySystemControllButton button, AbilitiesUIController abilitiesController, float scaleMultiplayer, Slider slider)
        {
            _energyController = controller;
            _uiPrefab = uiPrefab;
            _spawnParent = spawnParent;
            _backgroundImage = background;
            _controllButton = button;
            mAbilitiesUIController = abilitiesController;
            _scaleMultiplayer = scaleMultiplayer;
            _sliderPrefab = slider;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Object.Instantiate(_uiPrefab, _spawnParent);
            var bg = Object.Instantiate(_backgroundImage, ui.transform);
            bg.transform.localScale *= _scaleMultiplayer;
            var slider = Object.Instantiate(_sliderPrefab, bg.transform);
            slider.value = 0;
            var button = Object.Instantiate(_controllButton, slider.transform);
            mAbilitiesUIController.AddControlButton(button.GetComponent<InputSystemButtonBridge>(), slider);
            ui.ControllButton = button;
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