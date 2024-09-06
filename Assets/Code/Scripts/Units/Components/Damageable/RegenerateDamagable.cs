using System.Collections;
using UnityEngine;

namespace SustainTheStrain.Units
{
    public class RegenerateDamagable : Damageble
    {
        private float _lastDamageTime;
        private bool _calm = true;
        [SerializeField]
        private float _recoverDelay = 2;
        [SerializeField]
        private float _recoverySpeed = 0.3f;
        private Coroutine _recoveryCoroutine;

        public override void Damage(float damage, DamageType damageType)
        {
            _lastDamageTime = Time.time;
            _calm = false;
            
            base.Damage(damage, damageType);
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
        
        private void StartRecovery()
        {
            if( _recoveryCoroutine != null )
                StopCoroutine( _recoveryCoroutine );
            
            _recoveryCoroutine = StartCoroutine(RecoverHp());
        }

        private IEnumerator RecoverHp()
        {
            while(_calm && CurrentHP != MaxHP)
            {
                CurrentHP += _recoverySpeed * Time.deltaTime;
                
                yield return null;
            }
        }
    }
}
