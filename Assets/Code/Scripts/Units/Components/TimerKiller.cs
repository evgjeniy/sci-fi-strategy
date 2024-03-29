using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Damageble))]
    public class TimerKiller : MonoBehaviour
    {
        [SerializeField] private float _time = 5f;

        private Damageble _damageble;
        private Timer _timer = new();

        private void Start()
        {
            _damageble = GetComponent<Damageble>();
            
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
