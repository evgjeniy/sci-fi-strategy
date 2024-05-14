using SustainTheStrain.Abilities;
using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public class AbilityUIFactory : IFactory<IEnergySystem, EnergySystemUI>
    {
        private EnergySystemUI _uiPrefab;
        private Transform _spawnParent;
        private AbilityUIController _abilityUIController;   

        public AbilityUIFactory(EnergySystemUISettings settings, AbilityUIController abilityUIController, Transform spawnParent)
        {
            _uiPrefab = settings.UIPrefab;
            _spawnParent = spawnParent;
            _abilityUIController = abilityUIController;
        }
        
        public EnergySystemUI Create(IEnergySystem system)
        {
            var ui = Object.Instantiate(_uiPrefab, _spawnParent);
            var button = ui.ControllButton;
            var slider = button.transform.parent.GetComponentInChildren<Slider>();
            slider.value = 0;
            ui.SetIcon(system.EnergySettings.ButtonImage);
            ui.MaxBarsCount = system.EnergySettings.MaxEnergy;
            ui.Tip.IfNotNull(system.CacheUiTip);
            _abilityUIController.MakeSubscriptions(ui, system as IAbility, slider);
            return ui;
        }
    }
}