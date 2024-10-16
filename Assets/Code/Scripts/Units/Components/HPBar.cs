using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Damageble _damageble;
        [SerializeField] private Shield _shield;
        [SerializeField] private RectTransform _ui;
        [SerializeField] private RectTransform _hpBar;
        [SerializeField] private Transform _shieldBarsHolder;
        [SerializeField] private ShieldBar _shieldBarRef;
        [SerializeField] private float _cellOffset = 3f;
        [SerializeField] private float _maxHpSize;
        [SerializeField] private Transform _visual;
        [SerializeField] private UnityEngine.UI.Slider _slider;

        private List<ShieldBar> _shieldBars = new();

        private Vector3 _camForward;

        private void OnEnable()
        {
            _damageble.OnCurrentHPChanged += UpdateHP;
            if (_shield)
            {
                _shield.OnCellsCountChanged += RebuildShield;
                RebuildShield(_shield.CellsCount);
            }

            _camForward = Camera.main.transform.forward;

            UpdateHP(_damageble.CurrentHP);
        }

        private void Update()
        {

            if (_shield != null)
            {
                if (_shield.ShieldCells.Count <= 0 || _shield.ShieldCells[0].CurrentHP <= 0)
                    _visual.gameObject.Deactivate();
                else
                    _visual.gameObject.Activate();
            }
        }

        private void LateUpdate()
        {
            _ui.LookAt(_ui.position + _camForward);
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
            if (!_hpBar) return;
            _hpBar.gameObject.SetActive(!(Math.Abs(value - _damageble.MaxHP) < 0.1f));

            _slider.IfNotNull(x => x.value = value / _damageble.MaxHP);
        }
    }
}
