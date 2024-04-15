using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class RocketRotationState : TurretRotationState<Rocket>
    {
        public RocketRotationState(Damageble target, Vector3 startOrientation, Vector3 startPosition) 
            : base(target, startOrientation, startPosition, new RocketIdleState(), new RocketAttackState(target)) {}
    }
}