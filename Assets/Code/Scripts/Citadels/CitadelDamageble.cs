using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Citadels
{
    public class CitadelDamageble : Damageble
    {
        [SerializeField] private Shield _shield;

        public override void Damage(float damage)
        {
            if (_shield != null)
            {
                if(_shield.Damage(damage))
                {
                    return;
                }
            }
            CurrentHP -= damage;

            if (CurrentHP <= 0)
            {
                Die();
            }
        }

        protected override void Die()
        {
            InvokeOnDied();
        }
    }
}
