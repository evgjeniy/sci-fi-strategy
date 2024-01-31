using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SustainTheStrain.EnergySystem.Shield;

namespace SustainTheStrain.UI
{
    public class ShieldBarController : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;

        private ShieldCell _cell;

        public void Init(ShieldCell shieldCell)
        {
            if (shieldCell == null) return;
            _cell = shieldCell;
            _cell.OnCurrentHPChanged += UpdateValue;
            UpdateValue(_cell.CurrentHP);
        }

        public void UpdateValue(float value)
        {
            _text.text = string.Format("{0}/{1}", value, _cell.MaxHP);
            if (_slider != null)
            {
                _slider.value = value/_cell.MaxHP;
            }
        }
    }
}