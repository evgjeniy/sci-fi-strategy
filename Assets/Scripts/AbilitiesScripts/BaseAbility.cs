using UnityEngine;

public abstract class BaseAbility
{
    private const float eps = 0.00001f;
    protected float reload = 1f, loadingSpeed;

    public float getReload()
    {
        if (Mathf.Abs(reload - 1f) < eps)
            return 1f;
        return Mathf.Min(reload, 1f);
    }

    public void setLoadingSpeed(float speed) => loadingSpeed = speed; //ne znayu nuzhen li no pust budet))

    public void Shoot(RaycastHit hit)
    {
        if (!isReloaded())
        {
            FailShootLogic();
            return;
        }
        reload = 0;
        SuccessShootLogic(hit);
    }

    public void Load(float delt)
    {
        if (!isReloaded())
        {
            reload += loadingSpeed * delt;
            if(isReloaded())
                ReadyToShoot();
        }
    }

    public bool isReloaded() => getReload() == 1f; //reload > 1 and pogreshnost ychtena v getReload()

    protected abstract void FailShootLogic();

    protected abstract void SuccessShootLogic(RaycastHit hit);

    protected abstract void ReadyToShoot();

    public abstract void UpdateLogic(RaycastHit hit);

    public abstract void DestroyLogic();
}
