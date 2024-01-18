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

    protected override void SuccessShootLogic(Vector3 point)
    {
        Debug.Log("zoneDMG success shot");
    }

    protected override void ReadyToShoot()
    {
        Debug.Log("zoneDMG ready to shoot");
    }
}
