using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Damageble _damageble;
        [SerializeField] private Shield _shield;
        [SerializeField] private Transform _hpBar;
        [SerializeField] private Transform _shieldBarsHolder;
        [SerializeField] private ShieldBar _shieldBarRef;
        [SerializeField] private float _cellOffset = 3f;
        [SerializeField] private float _maxHpSize;
        
        private List<ShieldBar> _shieldBars = new();
        
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
            _hpBar.localScale = new Vector3(value / _damageble.MaxHP * _maxHpSize, 1, 1);
        }
    }
}
