using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ZoneDamageAbility : ZoneAbility
    {
        public ZoneDamageAbility(float zone, float speed)
        {
            ZoneRadius = zone;
            LoadingSpeed = speed;
        }

        protected override void FailShootLogic()
        {
            Debug.Log("zoneDGM failed to shoot");
        }

        protected override void SuccessShootLogic(RaycastHit hit)
        {
            Debug.Log("zoneDMG success shot");
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("zoneDMG ready to shoot");
        }

        public override void UpdateLogic(RaycastHit hit)
        {
            var point = hit.point;
            AimZone.transform.position = point + Offset;
            if (IsReloaded())
            {
                //ìá öâåò ïðèöåëà áóäåò çåëåíûé
                if (UnityEngine.Input.GetMouseButtonDown(0))
                    Shoot(hit);
            }
            else
            {
                //ìá öâåò ïðèöåëà áóäåò êðàñíûé
            }
        }
    }
}