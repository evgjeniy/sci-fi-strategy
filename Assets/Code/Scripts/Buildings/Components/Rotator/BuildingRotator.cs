using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public interface ISpawnPointProvider
    {
        public Transform SpawnPoint { get; }
    }

    public class BuildingRotator : MonoBehaviour, ISpawnPointProvider
    {
        [SerializeField] private Transform _horizontalRotationBone;
        [SerializeField] private Transform _verticalRotationBone;
        [SerializeField, MinMaxSlider(-90, 90)] private Vector2 _verticalClamp = new(-45, 45);

        [SerializeField] private Transform[] _projectileSpawnPoints;

        [Inject] private Observable<Vector3> _targetPosition;

        public Transform SpawnPoint => _projectileSpawnPoints[Random.Range(0, _projectileSpawnPoints.Length)];

        private void OnEnable() => _targetPosition.Changed += Rotate;
        private void OnDisable() => _targetPosition.Changed -= Rotate;


        private void Rotate(Vector3 targetPosition)
        {
            var lookRotation = Quaternion.LookRotation(targetPosition - _verticalRotationBone.position);

            var verticalAngle = lookRotation.eulerAngles.x;
            if (verticalAngle > 180) verticalAngle -= 360;
            verticalAngle = Mathf.Clamp(verticalAngle, _verticalClamp.x, _verticalClamp.y);

            _horizontalRotationBone.eulerAngles = Vector3.up * lookRotation.eulerAngles.y;
            _verticalRotationBone.localEulerAngles = Vector3.right * verticalAngle;
        }
    }
}