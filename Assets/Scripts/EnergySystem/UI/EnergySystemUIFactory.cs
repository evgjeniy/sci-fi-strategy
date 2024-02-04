using System;
using System.Collections.Generic;
using SustainTheStrain.AbilitiesScripts;
using SustainTheStrain.Input.UI;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SustainTheStrain.EnergySystem.UI
{
    public class EnergySystemUIFactory : MonoBehaviour
    {
        [NonSerialized] public AbilitiesUIController MAbilitiesUIController;
        [SerializeField] private EnergySystemUI _energySystemUIPrefab;
        [SerializeField] private EnergySystemControllButton _energySystemControllButton;
        [SerializeField] private Slider _abilityChargeSliderPrefab;
        [SerializeField] private Transform _standartUIParent;
        [SerializeField] private Transform _abilitiesUIParent;
        [SerializeField] private Image _standartBackgroundImageSprite;
        [SerializeField] private Image _abilityBackgroundImage;
        
        
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
            button.image.sprite = system.ButtonImage;
            ui.MaxBarsCount = system.MaxEnergy;
            button.OnLeftMouseClick += system.TrySpendEnergy;
            button.OnRightMouseClick += system.TryRefillEnergy;
            system.OnCurrentEnergyChanged += ui.ChangeEnergy;

            return new KeyValuePair<IEnergySystem, EnergySystemUI>(system, ui);

        }
    }
}