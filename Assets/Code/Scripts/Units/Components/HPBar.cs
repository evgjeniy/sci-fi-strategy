using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace SustainTheStrain.Units
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Damageble _damageble;
        [SerializeField] private Shield _shield;
        [SerializeField] private RectTransform _hpBar;
        [SerializeField] private Transform _shieldBarsHolder;
        [SerializeField] private ShieldBar _shieldBarRef;
        [SerializeField] private float _cellOffset = 3f;
        [SerializeField] private float _maxHpSize;
        [SerializeField] private Transform _visual;
        [SerializeField] private UnityEngine.UI.Slider _slider;

        private List<ShieldBar> _shieldBars = new();

        public static Vector3 _camForward;

        private void OnEnable()
        {
            _damageble.OnCurrentHPChanged += UpdateHP;
            if (_shield)
            {
                _shield.OnCellsCountChanged += RebuildShield;
                RebuildShield(_shield.CellsCount);
            }

            UpdateHP(_damageble.CurrentHP);
        }

        private void Update()
        {

            if (_shield != null)
            {
                if (_shield.ShieldCells[0].CurrentHP <= 0)
                    _visual.gameObject.Deactivate();
                else
                    _visual.gameObject.Activate();
            }
        }

        private void LateUpdate()
        {
            _hpBar.LookAt(_hpBar.position + _camForward);
        }

        private void OnDisable()
        {
            _damageble.OnCurrentHPChanged += UpdateHP; 
            if (_shield)
                _shield.OnCellsCountChanged += RebuildShield;
        }

        private void RebuildShield(int obj)
        {
            foreach (var bar in _shieldBars)
            {
                Destroy(bar.gameObject);
            }
            _shieldBars.Clear();

            for (var index = 0; index < _shield.ShieldCells.Count; index++)
            {
                var cell = _shield.ShieldCells[index];
                var cellBar = Instantiate(_shieldBarRef, _shieldBarsHolder);
                cellBar.transform.localPosition += Vector3.right * index * _cellOffset;
                cellBar.Init(cell);
                _shieldBars.Add(cellBar);
            }
        }

        private void UpdateHP(float value)
        {
            _hpBar.gameObject.SetActive(!(Math.Abs(value - _damageble.MaxHP) < 0.1f));

            _slider.value = value / _damageble.MaxHP;
        }
    }
}
