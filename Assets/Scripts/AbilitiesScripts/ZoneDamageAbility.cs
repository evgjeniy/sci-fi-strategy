using UnityEngine;

public class ZoneDamageAbility : BaseAbility
{
    private float zoneRadius;
    private GameObject aimSphere;

    public ZoneDamageAbility(float zone, GameObject aim, float speed)
    {
        zoneRadius = zone;
        aimSphere = aim;
        loadingSpeed = speed;
    }

    protected override void FailShootLogic()
    {
        Debug.Log("ZONE failed to shoot");
    }

    protected override void SuccessShootLogic()
    {
        Debug.Log("ZONE success shot");
    }

    protected override void ReadyToShoot()
    {
        Debug.Log("ZONE ready to shoot");
    }
}
