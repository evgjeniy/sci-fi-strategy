using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class BasicEnergySystemUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        [Inject] private EnergyController _energyController;
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;

        public BasicEnergySystemUIFactory(EnergySystemUISettings settings)
        {
            _uiPrefab = settings.UIPrefab;
            _spawnParent = settings.SpawnParent;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = GameObject.Instantiate(_uiPrefab, _spawnParent);
            var button = ui.ControllButton;
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