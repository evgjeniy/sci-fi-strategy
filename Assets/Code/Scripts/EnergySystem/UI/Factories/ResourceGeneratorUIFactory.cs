using SustainTheStrain.ResourceSystems;
using SustainTheStrain.ResourceSystems.UI;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class ResourceGeneratorUIFactory : MonoUIFactory
    {
        public override EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Instantiate(_uiPrefab, _spawnParent);
            var bg = Instantiate(_backgroundImage, ui.transform);
            var button = Instantiate(_controllButton, bg.transform);
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