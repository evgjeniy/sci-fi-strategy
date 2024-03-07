using System;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units
{
    public class HeroController : MonoEnergySystem
    {
        [Inject] private IHeroInput _heroInput;
        [Inject] private Hero _hero;
        
        public EnergyController EnergyController { get; set; }
        
        [Inject]
        private void Init(EnergyController controller)
        {
            EnergyController = controller;
            LoadSettings();
            EnergyController.AddEnergySystem(this);
            
        }
        
        private void OnEnable()
        {
            _heroInput.OnSelected += HeroSelected;
            _heroInput.OnPointerEnter += HeroSelected;
            _heroInput.OnMove += HeroMove;
        }

        private void OnDisable()
        {
            _heroInput.OnSelected -= HeroSelected;
            _heroInput.OnPointerEnter -= HeroSelected;
            _heroInput.OnMove -= HeroMove;
        }
        
        public void IncreaseMaxEnergy(int value)
        {
            MaxEnergy += value;
        }

        private void LoadSettings()
        {
            MaxEnergy = EnergySettings.MaxEnergy;
        }
        
        private void HeroMove(Hero arg1, RaycastHit arg2)
        {
            Debug.LogWarning("HeroMoved");
            _hero.Move(arg2.point);
        }
        
        private void HeroSelected(Hero obj)
        {
            Debug.LogWarning("HeroSelected");
        }

    }
}
