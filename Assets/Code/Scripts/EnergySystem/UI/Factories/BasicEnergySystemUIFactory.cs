using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class BasicEnergySystemUIFactory : MonoUIFactory
    {
        
        public override EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Instantiate(_uiPrefab, _spawnParent);
            var bg = Instantiate(_backgroundImage, ui.transform);
            var button = Instantiate(_controllButton, bg.transform);
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