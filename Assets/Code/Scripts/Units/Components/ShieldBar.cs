using SustainTheStrain.Units.Components;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units
{
    public class ShieldBar : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider _slider;
        [SerializeField] private RectTransform _shieldBar;

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
            if (_shieldBar == null) return;

            _shieldBar.gameObject.SetActive(value > 0);
            _slider.IfNotNull(x => x.value = value / _cell.MaxHP);
        }

        private void LateUpdate()
        {
            if (_shieldBar != null)
            {
                _shieldBar.LookAt(_shieldBar.position + Camera.main.transform.forward);
            }
        }
    }
}
