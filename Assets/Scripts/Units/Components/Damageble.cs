using System;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class Damageble : MonoBehaviour
    {
        [field:SerializeField]
        public float MaxHP { get; protected set; }
        [field: SerializeField]
        public float CurrentHP { get; protected set; }
        [field: SerializeField]
        public int Team { get; protected set; }

        public event Action<Damageble> OnDied;
        public event Action<float> OnCurrentHPChanged;

        private void Awake()
        {
            CurrentHP = MaxHP;
        }

        public void Damage(float damage)
        {
            CurrentHP -= damage;
            OnCurrentHPChanged?.Invoke(CurrentHP);

            if (CurrentHP < 0)
            {
                Die();
            }
        }

        public void Die()
        {
            OnDied?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
