using SustainTheStrain.Units.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Damageble _damageble;
        [SerializeField] private float _maxHpSize;
        private void OnEnable()
        {
            transform.localScale = new Vector3(_damageble.CurrentHP / _damageble.MaxHP * _maxHpSize, 1, 1);
            _damageble.OnCurrentHPChanged += (float value) => { transform.localScale = new Vector3(value / _damageble.MaxHP * _maxHpSize, 1, 1); };
        }
    }
}
