using System;
using System.Collections.Generic;
using SustainTheStrain.AbilitiesScripts;
using SustainTheStrain.Input.UI;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
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
        [SerializeField] private Image _backgroundSprite;
        
        
        public KeyValuePair<IEnergySystem, EnergySystemUI> CreateUI(IEnergySystem system)
        {
            var ui = Instantiate(_energySystemUIPrefab, _standartUIParent);
            var bg = Instantiate(_backgroundSprite, ui.transform);
            var button = Instantiate(_energySystemControllButton, bg.transform);
            ui.ControllButton = button;
            button.image.sprite = system.ButtonImage;
            ui.MaxBarsCount = system.MaxEnergy;
            button.OnLeftMouseClick += system.TrySpendEnergy;
            button.OnRightMouseClick += system.TryRefillEnergy;
            system.OnCurrentEnergyChanged += ui.ChangeEnergy;
            switch (system)
            {
                case BaseAbility:
                {   
                    var s = Instantiate(_abilityChargeSliderPrefab, button.transform);
                    s.value = 0;
                    bg.transform.localScale *= 1.4f;
                    MAbilitiesUIController.AddControlButton(button.GetComponent<InputSystemButtonBridge>(),s);
                    ui.transform.parent = _abilitiesUIParent;
                    break;
                }
                case ResourceGenerator generator:
                {
                    var generatorUI = new GeneratorUI(button.transform, generator);
                    break;
                }
                default:
                    break;
            }

            return new KeyValuePair<IEnergySystem, EnergySystemUI>(system, ui);

        }
    }
}