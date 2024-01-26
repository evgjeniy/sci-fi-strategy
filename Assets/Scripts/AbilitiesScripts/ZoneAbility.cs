using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain.AbilitiesScripts
{
    public abstract class ZoneAbility : BaseAbility
    {
        protected float zoneRadius;

        protected Collider[] GetColliders(Vector3 point) => Physics.OverlapSphere(point, zoneRadius);
    }
}
