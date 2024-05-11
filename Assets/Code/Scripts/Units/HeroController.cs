using SustainTheStrain.EnergySystem;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Units
{
    public class HeroController : MonoEnergySystem
    {
        [Inject] private Hero _hero;
        private TMP_Text _uiTip;

        public EnergyController EnergyController { get; set; }
        
        [Inject]
        private void Init(EnergyController controller)
        {
            EnergyController = controller;
            LoadSettings();
            EnergyController.AddEnergySystem(this);
            
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
        

        private void UpdateTip(IEnergySystem system)
        {
            _uiTip.IfNotNull(x => x.text = $"Active cells: {system.CurrentEnergy}");
        }

        public override void CacheUiTip(TMP_Text uiTip)
        {
            _uiTip = uiTip;
            UpdateTip(this);
        }
    }
}
