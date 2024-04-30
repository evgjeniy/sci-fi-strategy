using UnityEngine;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Shield))]
    public class ShieldDamageble : Damageble
    {
        private Shield _shield;

        private void OnEnable()
        {
            _shield = GetComponent<Shield>();
        }
        
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
        }
        
        public void DeepDamage(float damage)
        {
            CurrentHP -= damage;
        }
    }
}
