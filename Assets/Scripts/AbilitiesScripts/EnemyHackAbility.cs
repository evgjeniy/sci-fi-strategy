using UnityEngine;

public class EnemyHackAbility : PointAbility
{
    protected override void FailShootLogic()
    {
        Debug.Log("HACK failed to shoot");
    }

    protected override void SuccessShootLogic(Vector3 point)
    {
        Debug.Log("HACK success shot");
    }

    protected override void ReadyToShoot()
    {
        Debug.Log("HACK ready to shoot");
    }
}
