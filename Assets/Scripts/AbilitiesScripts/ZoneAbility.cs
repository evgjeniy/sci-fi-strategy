using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class ZoneAbility : BaseAbility
{
    protected float zoneRadius;
    protected GameObject aimZone;
    protected Vector3 offset = new Vector3(0, 0.8f, 0);

    public void setAimZone(GameObject zone)
    {
        aimZone = zone;
        aimZone.GetComponent<DecalProjector>().size = new Vector3(zoneRadius, zoneRadius, 25); //ilya skazal poh, pust budet 25
    }

    public override void DestroyLogic()
    {
        Object.Destroy(aimZone);
    }
}
