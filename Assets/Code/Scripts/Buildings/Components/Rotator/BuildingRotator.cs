using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BuildingRotator : MonoBehaviour
    {
        [SerializeField] private Transform _horizontalRotationBone;
        [SerializeField] private Transform _verticalRotationBone;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField, MinMaxSlider(-90, 90)] private Vector2 _verticalClamp = new(-45, 45);

        [Inject] private Observable<Vector3> _targetPosition;

        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;

        private void OnEnable() => _targetPosition.Changed += Rotate;
        private void OnDisable() => _targetPosition.Changed -= Rotate;

        private void Rotate(Vector3 targetPosition)
        {
            var lookRotation = Quaternion.LookRotation(targetPosition - _verticalRotationBone.position);

            var verticalAngle = lookRotation.eulerAngles.x;
            if (verticalAngle > 180) verticalAngle -= 360;
            verticalAngle = Mathf.Clamp(verticalAngle, _verticalClamp.x, _verticalClamp.y);

            _horizontalRotationBone.localEulerAngles = Vector3.up * lookRotation.eulerAngles.y;
            _verticalRotationBone.localEulerAngles = Vector3.right * verticalAngle;
        }
    }
}