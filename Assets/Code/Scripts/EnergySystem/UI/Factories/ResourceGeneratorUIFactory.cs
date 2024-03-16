using SustainTheStrain.ResourceSystems;
using SustainTheStrain.ResourceSystems.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class ResourceGeneratorUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        private EnergyController _energyController;
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        private Image _backgroundImage;
        private EnergySystemControllButton _controllButton;
        
        public ResourceGeneratorUIFactory(EnergyController controller, EnergySystemUI uiPrefab, Transform spawnParent,
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
            var ui = Object.Instantiate(_uiPrefab, _spawnParent);
            var bg = Object.Instantiate(_backgroundImage, ui.transform);
            var button = Object.Instantiate(_controllButton, bg.transform);
            var generatorUI = new GeneratorUI(button.transform, system as ResourceGenerator);
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
            system.Changed += ui.ChangeEnergy; //перенести в UIController
            return ui;
        }
    }
}