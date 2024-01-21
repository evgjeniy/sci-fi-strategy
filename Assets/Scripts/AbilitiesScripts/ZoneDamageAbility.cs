using System;
using UnityEngine;

public class ZoneDamageAbility : ZoneAbility
{
    public ZoneDamageAbility(float zone, float speed)
    {
        zoneRadius = zone;
        loadingSpeed = speed;
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
        Vector3 point = hit.point;
        aimZone.transform.position = point + offset;
        if (isReloaded())
        {
            //мб цвет прицела будет зеленый
            if (Input.GetMouseButtonDown(0))
                Shoot(hit);
        }
        else
        {
            //мб цвет прицела будет красный
        }
    }
}
