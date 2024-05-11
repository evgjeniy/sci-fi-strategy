using SustainTheStrain.EnergySystem;
using SustainTheStrain.Scriptable.EnergySettings;
using SustainTheStrain.Units;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Citadels
{
    public class CitadelShieldController : MonoEnergySystem
    {
        [SerializeField] private Shield _shield;
        
        private TMP_Text _uiTip;

        private void OnEnable() => Changed += UpdateCellsCount;
        private void OnDisable() => Changed -= UpdateCellsCount;
        private void UpdateCellsCount(IEnergySystem system)
        {
            _shield.CellsCount = system.CurrentEnergy;
            UpdateTip(system);
        }

        public override void SetEnergySettings(EnergySystemSettings settings)
        {
            EnergySettings = settings;
            _maxEnergy = settings.MaxEnergy;
            _currentEnergy = 0;
        }

        public override void CacheUiTip(TMP_Text uiTip) { _uiTip = uiTip; UpdateTip(this); }
        private void UpdateTip(IEnergySystem system)
        {
            _uiTip.IfNotNull(tip => tip.text = 
$@"<b><align=""center"">Щит цитадели (энергия: <color=""green"">{system.CurrentEnergy}</color>)</align></b>
Защищает цитадель от получаемого урона. С каждым вложенным
блоком энергии появляется ячейка защиты. Уничтоженные ячейки
щита восстанавливаются, если не получают урон.
Здоровье одной ячейки: <b><#55FF55>200 ед.</color></b>");
        }
    }
}