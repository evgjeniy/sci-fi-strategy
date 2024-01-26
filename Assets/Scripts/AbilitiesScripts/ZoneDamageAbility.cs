using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ZoneDamageAbility : ZoneAbility
    {
        public ZoneDamageAbility(float zone, float speed)
        {
            zoneRadius = zone;
            LoadingSpeed = speed;
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
                dmg.Damage(0.3f); //ya hz skolko nado damagit
                //Debug.Log(dmg.CurrentHP);
            }
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneDMG ready to shoot");
        }
    }
}
