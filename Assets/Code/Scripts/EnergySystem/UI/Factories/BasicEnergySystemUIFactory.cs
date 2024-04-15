using UnityEngine;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class BasicEnergySystemUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        private BaseEnergyUIController _energyUIController;

        public BasicEnergySystemUIFactory(EnergySystemUISettings settings, BaseEnergyUIController energyUIController, Transform spawnParent)
        {
            _uiPrefab = settings.UIPrefab;
            _spawnParent = spawnParent;
            _energyUIController = energyUIController;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = GameObject.Instantiate(_uiPrefab, _spawnParent);
            var button = ui.ControllButton;
            button.image.sprite = system.EnergySettings.ButtonImage;
            ui.MaxBarsCount = system.EnergySettings.MaxEnergy;
            _energyUIController.MakeSubscriptions(ui, system);
            return ui;
        }
    }
}