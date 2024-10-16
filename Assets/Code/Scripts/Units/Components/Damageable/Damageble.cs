using SustainTheStrain.Units.Components;
using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units
{
    public class Damageble : MonoBehaviour, IDamageable
    {
        [field: SerializeField] 
        [field: MinValue(0)] [field: MaxValue(100)] 
        public int DamageResistance { get; private set; }
        
        [SerializeField] public GameObject _afterDeath;

        [field:SerializeField]
        public float MaxHP { get; set; }

        public float CurrentHP
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Clamp(value, 0, MaxHP); 
                OnCurrentHPChanged?.Invoke(value);
                
                if (_currentHp <= 0.1f) Die();
            }
        }

        [field: SerializeField]
        public Team Team { get; set; }

        [field:SerializeField] 
        public bool IsFlying { get; set; }

        public event Action<Damageble> OnDied;
        public event Action<Damageble, bool> OnDiedResult;
        public event Action<float> OnCurrentHPChanged;

        public void InvokeOnDied()
        {
            OnDied?.Invoke(this);
        }

        private float _currentHp;

        private void Awake()
        {
            CurrentHP = MaxHP;
        }

        public virtual void Damage(float damage)
        {
            float dmg = (100 - DamageResistance) / 100f * damage;
            CurrentHP -= Mathf.Round(dmg);
        }

        public virtual void DeepDamage(float damage)
        {
            CurrentHP -= damage;
        }
        
        public void Kill(bool suicide = false)
        {
            Die(true);
        }
        
        protected virtual void Die(bool suicide = false)
        {
            OnDied?.Invoke(this);
            OnDiedResult?.Invoke(this, suicide);
            
            _afterDeath.IfNotNull(x => Instantiate(x, transform.position, Quaternion.identity));
            gameObject.Deactivate();
        }
    }
}
