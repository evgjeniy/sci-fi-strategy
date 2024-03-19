using SustainTheStrain.ResourceSystems;
using SustainTheStrain.ResourceSystems.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class ResourceGeneratorUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        [Inject] private EnergyController _energyController;
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        
        public ResourceGeneratorUIFactory(EnergySystemUISettings settings)
        {
            _uiPrefab = settings.UIPrefab;
            _spawnParent = settings.SpawnParent;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Object.Instantiate(_uiPrefab, _spawnParent);
            var button = ui.ControllButton;
            var generatorUI = new GeneratorUI(button.transform, system as ResourceGenerator);
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