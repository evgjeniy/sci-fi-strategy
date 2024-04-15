using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryRotationState : TurretRotationState<Artillery>
    {
        public ArtilleryRotationState(Damageble target, Vector3 startOrientation, Vector3 startPosition) 
            : base(target, startOrientation, startPosition, new ArtilleryIdleState(), new ArtilleryAttackState(target)) {}
    }
}