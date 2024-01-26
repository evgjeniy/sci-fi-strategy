using SustainTheStrain.EnergySystem;
using SustainTheStrain.Units.Components;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.UI
{
    public class CitadelUIController : MonoBehaviour
    {
        [SerializeField] private Damageble _damageble;
        [SerializeField] private Shield _shield;

        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private RectTransform _shieldBarsHolder;

        [SerializeField] private ShieldBarController _shieldBarRef;

        private void Start()
        {
            foreach(var cell in _shield.ShieldCells)
            {
                var cellUI = Instantiate(_shieldBarRef, _shieldBarsHolder);
                cellUI.Init(cell);
            }

            _damageble.OnCurrentHPChanged += UpdateHP; 
        }

        private void UpdateHP(float value)
        {
            _text.text = string.Format("{0}/{1}", value, _damageble.MaxHP);
            if (_healthSlider != null)
            {
                _healthSlider.value = value / _damageble.MaxHP;
            }
        }
    }
}
