using System;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class Damageble : MonoBehaviour, IHealth, ITeam
    {
        [field:SerializeField]
        public float MaxHP { get; set; }

        public float CurrentHP
        {
            get { return _currentHp; }
            set { _currentHp = value; OnCurrentHPChanged?.Invoke(value); }
        }

        [field: SerializeField]
        public int Team { get; set; }

        public event Action<Damageble> OnDied;
        public event Action<float> OnCurrentHPChanged;

        private float _currentHp;

        private void Awake()
        {
            CurrentHP = MaxHP;
        }

        public virtual void Damage(float damage)
        {
            CurrentHP -= damage;
            OnCurrentHPChanged?.Invoke(CurrentHP);

            if (CurrentHP < 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            OnDied?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
