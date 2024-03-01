using System;
using Dreamteck.Splines.Primitives;
using SustainTheStrain.Buildings.FSM;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    [RequireComponent(typeof(Damageable))]
    public class TimerKiller : MonoBehaviour
    {
        [SerializeField] private float _time = 5f;

        private IDamageable _damageble;
        private Timer _timer = new();

        private void Start()
        {
            _damageble = GetComponent<IDamageable>();
            
            _timer.Time = _time;
        }

        private void Update()
        {
            _timer.Time -= Time.deltaTime;
            
            if (_timer.IsTimeOver)
                _damageble.Die();
        }
    }
}
