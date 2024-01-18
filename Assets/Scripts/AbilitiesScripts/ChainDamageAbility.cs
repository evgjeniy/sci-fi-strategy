using UnityEngine;

public class ChainDamageAbility : PointAbility
{
    protected override void FailShootLogic()
    {
        Debug.Log("CHAIN failed to shoot");
    }

    protected override void SuccessShootLogic(Vector3 point)
    {
        Debug.Log("CHAIN success shot");
    }

    protected override void ReadyToShoot()
    {
        Debug.Log("CHAIN ready to shoot");
    }
}
