using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BuildingRotator : MonoBehaviour
    {
        [SerializeField] private Transform _horizontalRotationBone;
        [SerializeField] private Transform _verticalRotationBone;
        [SerializeField, MinMaxSlider(-90, 90)] private Vector2 _verticalClamp = new(-45, 45);
        
        [Inject] private Observable<Vector3> _targetPosition;

        private void OnEnable() => _targetPosition.Changed += Rotate;
        private void OnDisable() => _targetPosition.Changed -= Rotate;

        private void Rotate(Vector3 targetPosition)
        {
            var lookRotation = Quaternion.LookRotation(targetPosition - _verticalRotationBone.position);
            var lookRotationEuler = lookRotation.eulerAngles; // TODO : fix, returns incorrect euler angle

            var horizontalAngle = lookRotationEuler.y;
            var verticalAngle = Mathf.Clamp(lookRotationEuler.x, _verticalClamp.x, _verticalClamp.y);

            _horizontalRotationBone.localEulerAngles = Vector3.up * horizontalAngle;
            _verticalRotationBone.localEulerAngles = Vector3.right * verticalAngle;
        }
    }
}