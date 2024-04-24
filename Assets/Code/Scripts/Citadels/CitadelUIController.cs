using System.Collections.Generic;
using SustainTheStrain.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.Citadels
{
    public class CitadelUIController : MonoBehaviour
    {
        [SerializeField] private Damageble _damageble;
        [SerializeField] private Shield _shield;

        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private RectTransform _shieldBarsHolder;

        [SerializeField] private ShieldBarController _shieldBarRef;

        private List<ShieldBarController> _shieldBars = new();
        
        private void OnEnable()
        {
            _damageble.OnCurrentHPChanged += UpdateHP; 
            _shield.OnCellsCountChanged += RebuildShieldUI;
        }

        private void OnDisable()
        {
            _damageble.OnCurrentHPChanged += UpdateHP; 
            _shield.OnCellsCountChanged += RebuildShieldUI;
        }

        private void RebuildShieldUI(int obj)
        {
            foreach (var bar in _shieldBars)
            {
                Destroy(bar.gameObject);
            }
            _shieldBars.Clear();
            
            foreach(var cell in _shield.ShieldCells)
            {
                var cellUI = Instantiate(_shieldBarRef, _shieldBarsHolder);
                cellUI.Init(cell);
                _shieldBars.Add(cellUI);
            }
        }

        private void UpdateHP(float value)
        {
            //_text.text = string.Format("{0}/{1}", value, _damageble.MaxHP);
            if (_healthSlider != null)
            {
                _healthSlider.value = value / _damageble.MaxHP;
            }
        }
    }
}
