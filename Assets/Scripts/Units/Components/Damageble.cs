using System;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class Damageble : MonoBehaviour, IHealth, ITeam
    {
        [field:SerializeField]
        public float MaxHP { get; set; }
        [field: SerializeField]
        public float CurrentHP { get; set; }
        [field: SerializeField]
        public int Team { get; set; }

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
