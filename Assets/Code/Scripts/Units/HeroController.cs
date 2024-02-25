using System;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Scriptable.EnergySettings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units
{
    public class HeroController : MonoBehaviour, IEnergySystem
    {
        [Inject] private IHeroInput _heroInput;
        [Inject] private Hero _hero;
        
         public EnergyController EnergyController { get; set; }
        [field:SerializeField] public EnergySystemSettings EnergySettings { get; private set; }
        public Sprite ButtonImage => EnergySettings.ButtonImage;
        public int FreeEnergyCellsCount => MaxEnergy - CurrentEnergy;
        
        private int _currentEnergy;
        private int _maxEnergy;

        [Inject]
        private void Init(EnergyController controller)
        {
            EnergyController = controller;
            LoadSettings();
            EnergyController.AddEnergySystem(this);
            
        }
        
        public int CurrentEnergy
        {
            get => _currentEnergy;
            set
            {
                if (value < 0 || value > MaxEnergy) return;
                _currentEnergy = value;
                Changed?.Invoke(this);
            }
        }
        
        public int MaxEnergy {
            get =>_maxEnergy;
            private set => _maxEnergy = value;
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

        public bool TrySpendEnergy()
        {
            return true;
        }

        public bool TryRefillEnergy()
        {
            return true;
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

        public event Action<IEnergySystem> Changed;
    }
}
