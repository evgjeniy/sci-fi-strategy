using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ZoneDamageAbility : ZoneAbility
    {
        protected float damage;
        public ZoneDamageAbility(float zone, float speed, float dmg)
        {
            zoneRadius = zone;
            LoadingSpeed = speed;
            damage = dmg;
        }

        protected override void FailShootLogic()
        {
            Debug.Log("zoneDGM failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit)
        {
            Collider[] colliders = GetColliders(hit.point);
            for(int i = 0; i < colliders.Length; i++)
            {
                var dmg = colliders[i].GetComponent<Units.Components.Damageble>();
                if (dmg == null) continue;
                dmg.Damage(damage);
                //Debug.Log(dmg.CurrentHP);
            }
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneDMG ready to shoot");
        }
    }
}
