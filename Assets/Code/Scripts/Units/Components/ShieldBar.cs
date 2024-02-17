using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain
{
    public class ShieldBar : MonoBehaviour
    {
        [SerializeField] private Transform _bar;
        [SerializeField] private float _maxHpSize = 2f;

        private Shield.ShieldCell _cell;

        public void Init(Shield.ShieldCell shieldCell)
        {
            if (shieldCell == null) return;
            _cell = shieldCell;
            _cell.OnCurrentHPChanged += UpdateValue;
            UpdateValue(_cell.CurrentHP);
        }

        private void UpdateValue(float value)
        {
            if (_bar != null)
            {
                _bar.gameObject.SetActive(value > 0);
                _bar.localScale = new Vector3(value / _cell.MaxHP * _maxHpSize, 1, 1);;
            }
        }
    }
}
