using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class TurretRotationState<TTurret> : IUpdatableState<TTurret> where TTurret : ITurret
    {
        private readonly IUpdatableState<TTurret> _idleState;
        private readonly IUpdatableState<TTurret> _attackState;

        private readonly Damageble _target;
        private readonly Vector3 _startDirection;
        private readonly float _rotationTime;

        private float _elapsedTime;

        public TurretRotationState(Damageble target, Vector3 startOrientation, Vector3 startPosition,
            IUpdatableState<TTurret> idleState, IUpdatableState<TTurret> attackState)
        {
            _target = target;
            _idleState = idleState;
            _attackState = attackState;
            _startDirection = startOrientation - startPosition;
            _rotationTime = Vector3.Angle(_startDirection, target.transform.position - startPosition) / Const.RotationSpeed;
        }

        public IUpdatableState<TTurret> Update(TTurret turret)
        {
            var rocketPosition = turret.transform.position;
            turret.Area.Update(rocketPosition, turret.Config.Radius, turret.Config.Mask);
            
            if (_target.IsNotIn(turret.Area))
                return _idleState;

            var time = _elapsedTime / _rotationTime;
            var targetDirection = _target.transform.position - rocketPosition;
            turret.Orientation = rocketPosition + Vector3.Lerp(_startDirection, targetDirection, time);
            
            if (time >= 1.0f)
                return _attackState;

            _elapsedTime += Time.deltaTime;
            return this;
        }
    }
}