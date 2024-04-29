using System.Collections;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class StunEffect : MonoBehaviour
    {
        private Coroutine _routine;
        private Unit _unit;
        private float _oldSpeed;

        public void Initialize(float duration)
        {
            if (TryGetComponent(out _unit) is false)
            {
                Destroy(this);
            }
            else
            {
                if (_routine == null) _oldSpeed = _unit.CurrentPathFollower.Speed;
                else StopCoroutine(_routine);

                _routine = StartCoroutine(DeactivateShield(duration));
            }
        }

        private IEnumerator DeactivateShield(float duration)
        {
            _unit.Freeze();
            yield return new WaitForSeconds(duration);

            Destroy(this);
        }

        private void OnDestroy() => _unit.Unfreeze(_oldSpeed);
    }
}