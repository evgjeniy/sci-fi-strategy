using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class LaserRotationState : IUpdatableState<Laser>
    {
        private readonly Damageble _target;
        private readonly Vector3 _startDirection;
        private readonly float _rotationTime;

        private float _elapsedTime;

        public LaserRotationState(Damageble target, Vector3 startOrientation, Vector3 startPosition)
        {
            _target = target;
            _startDirection = startOrientation - startPosition;
            _rotationTime = Vector3.Angle(_startDirection, target.transform.position - startPosition) / Const.RotationSpeed;
        }

        public IUpdatableState<Laser> Update(Laser laser)
        {
            var rocketPosition = laser.transform.position;
            laser.Area.Update(rocketPosition, laser.Config.Radius, laser.Config.Mask);
            
            if (_target.IsNotIn(laser.Area))
                return new LaserIdleState();

            var time = _elapsedTime / _rotationTime;
            var targetDirection = _target.transform.position - rocketPosition;
            laser.Orientation = rocketPosition + Vector3.Lerp(_startDirection, targetDirection, time);
            
            if (time >= 1.0f)
                return new LaserAttackState(_target);

            _elapsedTime += Time.deltaTime;
            return this;
        }
    }
}