using SustainTheStrain.Units.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SustainTheStrain.EnergySystem
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private int _cellsCount = 2;
        [SerializeField] private float _cellHp = 6;
        [SerializeField] private float _recoverDelay = 5;
        [SerializeField] private float _recoverySpeed = 0.3f;

        private List<ShieldCell> _shieldCells = new List<ShieldCell>();

        public List<ShieldCell> ShieldCells => _shieldCells;

        private float _lastDamageTime;
        private bool _calm = true;

        private Coroutine _recoveryCoroutine;


        private void Awake()
        {
            for(int i = 0; i < _cellsCount; i++)
            {
                _shieldCells.Add(new ShieldCell(_cellHp));
            }
        }

        private void Update()
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
                CurrentHP = MaxHP;
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
