using UnityEngine;

public class ZoneSlownessAbility : ZoneAbility
{
    protected override void FailShootLogic()
    {
        Debug.Log("zoneSLOW failed to shoot");
    }

    protected override void SuccessShootLogic(Vector3 point)
    {
        Debug.Log("zoneSLOW success shot");
    }

    protected override void ReadyToShoot()
    {
        Debug.Log("zoneSLOW ready to shoot");
    }
}
