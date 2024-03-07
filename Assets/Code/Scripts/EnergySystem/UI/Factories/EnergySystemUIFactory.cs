/*using System;
using System.Collections.Generic;
using SustainTheStrain.Abilities;
using SustainTheStrain.Input.UI;
using SustainTheStrain.ResourceSystems;
using SustainTheStrain.ResourceSystems.UI;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.EnergySystem.UI
{
    public class EnergySystemUIFactory : MonoBehaviour //non monobeh
    {
        [NonSerialized] public AbilitiesUIController MAbilitiesUIController;
        [SerializeField] private EnergySystemUI _energySystemUIPrefab;
        [SerializeField] private EnergySystemControllButton _energySystemControllButton;
        [SerializeField] private Slider _abilityChargeSliderPrefab;
        [SerializeField] private Transform _standartUIParent;
        [SerializeField] private Transform _abilitiesUIParent;
        [SerializeField] private Image _standartBackgroundImageSprite;
        [SerializeField] private Image _abilityBackgroundImage;


        private EnergyController _energyController;
        
        public KeyValuePair<IEnergySystem, EnergySystemUI> CreateUI(IEnergySystem system)
        {
            var ui = Instantiate(_energySystemUIPrefab, _standartUIParent);
            Image bg;
            EnergySystemControllButton button = null;
            
            switch (system)
            {
                case BaseAbility:
                {
                    bg = Instantiate(_abilityBackgroundImage, ui.transform);
                    var buttonTransform = bg.transform;
                    buttonTransform.localScale *= 1.4f;
                    var slider = Instantiate(_abilityChargeSliderPrefab, buttonTransform);
                    slider.value = 0;
                    button = Instantiate(_energySystemControllButton, slider.transform);
                    MAbilitiesUIController.AddControlButton(button.GetComponent<InputSystemButtonBridge>(),slider);
                    ui.transform.parent = _abilitiesUIParent;
                    break;
                }
                case ResourceGenerator generator:
                {
                    bg = Instantiate(_standartBackgroundImageSprite, ui.transform);
                    button = Instantiate(_energySystemControllButton, bg.transform);
                    var generatorUI = new GeneratorUI(button.transform, generator);
                    break;
                }
                default:
                    bg = Instantiate(_standartBackgroundImageSprite, ui.transform);
                    button = Instantiate(_energySystemControllButton, bg.transform);
                    break;
            }
            
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

            return new KeyValuePair<IEnergySystem, EnergySystemUI>(system, ui);
        }
    }
}*/