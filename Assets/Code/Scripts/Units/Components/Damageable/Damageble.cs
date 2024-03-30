using SustainTheStrain.Units.Components;
using System;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units
{
    public class Damageble : MonoBehaviour, IDamageable
    {
        [SerializeField] public GameObject _afterDeath;

        [field:SerializeField]
        public float MaxHP { get; set; }

        public float CurrentHP
        {
            get => _currentHp;
            set { _currentHp = Mathf.Clamp(value, 0, MaxHP); OnCurrentHPChanged?.Invoke(value); }
        }

        [field: SerializeField]
        public Team Team { get; set; }

        public event Action<Damageble> OnDied;
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
            CurrentHP -= damage;
            OnCurrentHPChanged?.Invoke(CurrentHP);

            if (CurrentHP <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            OnDied?.Invoke(this);

            _afterDeath.IfNotNull(x => Instantiate(x, transform.position, Quaternion.identity));
            Destroy(gameObject);
        }
    }
}
