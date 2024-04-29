using System.Collections;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class FireEffect : MonoBehaviour
    {
        private float _damagePerSecond;
        private float _duration;
        private float _startTime;
        
        private Coroutine _routine;
        private IDamageable _damageable;

        public void Initialize(float damagePerSecond, float duration)
        {
            _damagePerSecond = damagePerSecond;
            _duration = duration;
            _startTime = Time.time;

            if (TryGetComponent(out _damageable) is false)
            {
                Destroy(this);
                return;
            }
            
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(TakeFireDamage());
        }

        private IEnumerator TakeFireDamage()
        {
            _damageable.Damage(_damagePerSecond);
            
            if (Time.time - _startTime > _duration) 
                Destroy(this);

            yield return new WaitForSeconds(1.0f);
        }
    }
}