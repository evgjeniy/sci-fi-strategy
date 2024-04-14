using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class RocketRotationState : IUpdatableState<Rocket>
    {
        private readonly Damageble _target;
        private readonly Vector3 _startDirection;
        private readonly float _rotationTime;

        private float _elapsedTime;

        public RocketRotationState(Damageble target, Vector3 startOrientation, Vector3 startPosition)
        {
            _target = target;
            _startDirection = startOrientation - startPosition;
            _rotationTime = Vector3.Angle(_startDirection, target.transform.position - startPosition) / Const.RotationSpeed;
        }

        public IUpdatableState<Rocket> Update(Rocket rocket)
        {
            var rocketPosition = rocket.transform.position;
            rocket.Area.Update(rocketPosition, rocket.Config.Radius, rocket.Config.Mask);
            
            if (_target.IsNotIn(rocket.Area))
                return new RocketIdleState();

            var time = _elapsedTime / _rotationTime;
            var targetDirection = _target.transform.position - rocketPosition;
            rocket.Orientation = rocketPosition + Vector3.Lerp(_startDirection, targetDirection, time);
            
            if (time >= 1.0f)
                return new RocketAttackState(_target);

            _elapsedTime += Time.deltaTime;
            return this;
        }
    }
}