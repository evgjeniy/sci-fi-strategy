using System.Collections;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class ShieldDeactivatorEffect : MonoBehaviour
    {
        private float _duration;

        private Coroutine _routine;
        private Shield _shield;
        private int _shieldCells;

        public void Initialize(float duration)
        {
            _duration = duration;

            if (TryGetComponent(out _shield) is false)
            {
                Destroy(this);
                return;
            }

            if (_routine == null)
            {
                _shieldCells = _shield.CellsCount;
            }
            else
            {
                StopCoroutine(_routine);
                _shield.CellsCount = _shieldCells;
            }

            _routine = StartCoroutine(DeactivateShield());
        }

        private IEnumerator DeactivateShield()
        {
            _shield.CellsCount = 0;

            yield return new WaitForSeconds(_duration);

            _shield.CellsCount = _shieldCells;
        }
    }
}