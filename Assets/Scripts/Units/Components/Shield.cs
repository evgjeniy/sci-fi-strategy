using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class Shield : MonoBehaviour
    {
        [SerializeField]
        private int _cellsCount = 0;

        public int CellsCount
        {
            get => _cellsCount;
            set
            {
                _cellsCount = value;
                Debug.LogWarning($"Shield cells {_cellsCount}");
                if(_shieldCells.Count > _cellsCount)
                    while (_cellsCount < _shieldCells.Count)
                    {
                        _shieldCells.RemoveAt(_shieldCells.Count - 1);
                    }
                else
                {
                    while (_cellsCount > _shieldCells.Count)
                    {
                        _shieldCells.Add(new ShieldCell(_cellHp));
                    }
                }
                OnCellsCountChanged?.Invoke(_cellsCount);
                _calm = false;
            }
        }

        [SerializeField] private float _cellHp = 6;
        [SerializeField] private float _recoverDelay = 5;
        [SerializeField] private float _recoverySpeed = 0.3f;

        private List<ShieldCell> _shieldCells = new List<ShieldCell>();

        public List<ShieldCell> ShieldCells => _shieldCells;

        private float _lastDamageTime = 0;
        private bool _calm = true;

        private Coroutine _recoveryCoroutine;

        public Action<int> OnCellsCountChanged;

        private void Awake()
        {
            CellsCount = _cellsCount;
            
            /*for(int i = 0; i < _cellsCount; i++)
            {
                _shieldCells.Add(new ShieldCell(_cellHp));
            }*/
        }

        private void Update()
        {
            CheckAndRecover();
        }

        private void CheckAndRecover()
        {
            if(!_calm && Time.time - _lastDamageTime  > _recoverDelay)
            {
                _calm = true;
                StartRecovery();
            }
        }

        public bool Damage(float damage)
        {
            _lastDamageTime = Time.time;
            _calm = false;

            for (int i = _shieldCells.Count - 1; i >= 0; i--)
            {
                if (_shieldCells[i].CurrentHP > 0)
                {
                    _shieldCells[i].Damage(damage);
                    return true;
                }
            }
            return false;
        }

        private void StartRecovery()
        {
            if( _recoveryCoroutine != null )
                StopCoroutine( _recoveryCoroutine );

            if (_shieldCells.Count == 0) return;
            _recoveryCoroutine = StartCoroutine(RecoverShieldCells());
        }

        private IEnumerator RecoverShieldCells()
        {
            while(_calm && !_shieldCells.Last().IsFull)
            {
                foreach(var cell in _shieldCells)
                {
                    if(!cell.IsFull)
                    {
                        cell.Recover(_recoverySpeed * Time.deltaTime); 
                        break;
                    }
                }
                yield return null;
            }
        }

        public class ShieldCell : IHealth
        {
            public float MaxHP { get; set; }
            public float CurrentHP { get; set; }

            public event Action<ShieldCell> OnCellBroke;
            public event Action<float> OnCurrentHPChanged;

            public bool IsFull => CurrentHP == MaxHP;

            public ShieldCell(float maxHp)
            {
                MaxHP = maxHp;
                CurrentHP = 0;
            }

            public void Damage(float damage)
            {
                CurrentHP = Math.Clamp(CurrentHP - damage, 0, MaxHP);
                if(CurrentHP <= 0)
                {
                    OnCellBroke?.Invoke(this);
                }
                OnCurrentHPChanged?.Invoke(CurrentHP);
            }

            public void Recover(float value)
            {
                CurrentHP = Math.Clamp(CurrentHP + value, CurrentHP, MaxHP);
                OnCurrentHPChanged?.Invoke(CurrentHP);
            }
        }
    }
}
