using SustainTheStrain.ResourceSystems;
using SustainTheStrain.ResourceSystems.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class ResourceGeneratorUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        private ResourceGeneratorUIController _uiController;
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        
        public ResourceGeneratorUIFactory(EnergySystemUISettings settings, ResourceGeneratorUIController controller, Transform spawnParent)
        {
            _uiPrefab = settings.UIPrefab;
            _spawnParent = spawnParent;
            _uiController = controller;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Object.Instantiate(_uiPrefab, _spawnParent);
            var button = ui.ControllButton;
            button.image.sprite = system.EnergySettings.ButtonImage;
            ui.MaxBarsCount = system.EnergySettings.MaxEnergy;
            _uiController.MakeSubscriptions(ui,system);
            return ui;
        }
    }
}