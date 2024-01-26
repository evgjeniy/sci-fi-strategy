using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ZoneSlownessAbility : ZoneAbility
    {
        protected float speedCoefficient;

        public ZoneSlownessAbility(float zone, float speed, float koef)
        {
            zoneRadius = zone;
            LoadingSpeed = speed;
            speedCoefficient = koef;
        }

        protected override void FailShootLogic()
        {
            Debug.Log("zoneSLOW failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit)
        {
            Collider[] colliders = GetColliders(hit.point);
            for (int i = 0; i < colliders.Length; i++)
            {
                var dmg = colliders[i].GetComponent<Units.Unit>().CurrentPathFollower;
                if (dmg == null) continue;
                dmg.Speed *= speedCoefficient;
                //Debug.Log(dmg.Speed);
            }
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneSLOW ready to shoot");
        }
    }
}
