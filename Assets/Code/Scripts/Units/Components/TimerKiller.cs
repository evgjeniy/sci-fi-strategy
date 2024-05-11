using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Damageble))]
    public class TimerKiller : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        private IDamageable _damageble;

        private void Start() => _damageble = GetComponent<IDamageable>();

        private void Update()
        {
            _timer.Tick();
            
            if (_timer.IsOver)
                _damageble.Kill(true);
        }
    }
}
