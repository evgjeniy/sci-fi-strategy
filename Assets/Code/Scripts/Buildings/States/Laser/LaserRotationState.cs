using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class LaserRotationState : TurretRotationState<Laser>
    {
        public LaserRotationState(Damageble target, Vector3 startOrientation, Vector3 startPosition)
            : base(target, startOrientation, startPosition, new LaserIdleState(), new LaserAttackState(target)) {}
    }
}