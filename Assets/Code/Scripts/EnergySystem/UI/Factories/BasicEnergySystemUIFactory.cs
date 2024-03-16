using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class BasicEnergySystemUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        private EnergyController _energyController;
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        private Image _backgroundImage;
        private EnergySystemControllButton _controllButton;

        public BasicEnergySystemUIFactory(EnergyController controller, EnergySystemUI uiPrefab, Transform spawnParent,
            Image background, EnergySystemControllButton button)
        {
            _energyController = controller;
            _uiPrefab = uiPrefab;
            _spawnParent = spawnParent;
            _backgroundImage = background;
            _controllButton = button;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = GameObject.Instantiate(_uiPrefab, _spawnParent);
            var bg = GameObject.Instantiate(_backgroundImage, ui.transform);
            var button = GameObject.Instantiate(_controllButton, bg.transform);
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