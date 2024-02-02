using SustainTheStrain.EnergySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Components
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

        public override void Die()
        {
            InvokeOnDied();
        }
    }
}
