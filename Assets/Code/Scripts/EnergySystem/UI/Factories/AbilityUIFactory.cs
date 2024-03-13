using SustainTheStrain.Abilities;
using SustainTheStrain.Input.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class AbilityUIFactory : MonoUIFactory
    {
        [SerializeField] private AbilitiesUIController mAbilitiesUIController;
        [SerializeField] private float _scaleMultiplayer;
        [SerializeField] private Slider _sliderPrefab;
        
        public override EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Instantiate(_uiPrefab, _spawnParent);
            var bg = Instantiate(_backgroundImage, ui.transform);
            bg.transform.localScale *= _scaleMultiplayer;
            var slider = Instantiate(_sliderPrefab, bg.transform);
            slider.value = 0;
            var button = Instantiate(_controllButton, slider.transform);
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