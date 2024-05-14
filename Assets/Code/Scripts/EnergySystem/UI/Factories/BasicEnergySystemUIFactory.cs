using UnityEngine;
using UnityEngine.Extensions;
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
            ui.SetIcon(system.EnergySettings.ButtonImage);
            ui.MaxBarsCount = system.EnergySettings.MaxEnergy;
            ui.Tip.IfNotNull(system.CacheUiTip);
            _energyUIController.MakeSubscriptions(ui, system);
            return ui;
        }
    }
}