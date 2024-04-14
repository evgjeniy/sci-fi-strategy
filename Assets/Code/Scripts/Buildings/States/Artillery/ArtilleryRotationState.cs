using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryRotationState : IUpdatableState<Artillery>
    {
        private readonly Damageble _target;
        private readonly Vector3 _startDirection;
        private readonly float _rotationTime;

        private float _elapsedTime;

        public ArtilleryRotationState(Damageble target, Vector3 startOrientation, Vector3 startPosition)
        {
            _target = target;
            _startDirection = startOrientation - startPosition;
            _rotationTime = Vector3.Angle(_startDirection, target.transform.position - startPosition) / Const.RotationSpeed;
        }

        public IUpdatableState<Artillery> Update(Artillery artillery)
        {
            var rocketPosition = artillery.transform.position;
            artillery.Area.Update(rocketPosition, artillery.Config.Radius, artillery.Config.Mask);
            
            if (_target.IsNotIn(artillery.Area))
                return new ArtilleryIdleState();

            var time = _elapsedTime / _rotationTime;
            var targetDirection = _target.transform.position - rocketPosition;
            artillery.Orientation = rocketPosition + Vector3.Lerp(_startDirection, targetDirection, time);
            
            if (time >= 1.0f)
                return new ArtilleryAttackState(_target);

            _elapsedTime += Time.deltaTime;
            return this;
        }
    }
}