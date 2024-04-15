using UnityEngine;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Damageble))]
    public class TimerKiller : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        private Damageble _damageble;

        private void Start() => _damageble = GetComponent<Damageble>();

        private void Update()
        {
            _timer.ResetTime(_timer.Time - Time.deltaTime);
            
            if (_timer.IsTimeOver)
                _damageble.Die();
        }
    }
}
