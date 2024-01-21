using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain.AbilitiesScripts
{
    public abstract class ZoneAbility : BaseAbility
    {
        protected float ZoneRadius;
        protected GameObject AimZone;
        protected Vector3 Offset = new(0, 0.8f, 0);

        public void SetAimZone(GameObject zone)
        {
            AimZone = zone;
            AimZone.GetComponent<DecalProjector>().size = new Vector3(ZoneRadius, ZoneRadius, 25); // ilya skazal poh, pust budet 25
        }

        public override void DestroyLogic()
        {
            Object.Destroy(AimZone);
        }
    }
}
