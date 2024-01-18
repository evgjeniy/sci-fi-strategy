using UnityEngine;

public class LandingAbility : PointAbility
{
    protected override void FailShootLogic()
    {
        Debug.Log("LAND failed to shoot");
    }

    protected override void SuccessShootLogic(Vector3 point)
    {
        Debug.Log("LAND success shot");
    }

    protected override void ReadyToShoot()
    {
        Debug.Log("LAND ready to shoot");
    }
}
