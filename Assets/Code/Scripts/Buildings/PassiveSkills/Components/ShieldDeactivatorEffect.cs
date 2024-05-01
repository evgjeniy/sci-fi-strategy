using System.Collections;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class ShieldDeactivatorEffect : MonoBehaviour
    {
        private Coroutine _routine;
        private Shield _shield;
        private int _shieldCells;

        public void Initialize(float duration)
        {
            if (TryGetComponent(out _shield) is false)
            {
                Destroy(this);
            }
            else
            {
                if (_routine == null) _shieldCells = _shield.CellsCount;
                else StopCoroutine(_routine);

                _routine = StartCoroutine(DeactivateShield(duration));
            }
        }

        private IEnumerator DeactivateShield(float duration)
        {
            _shield.CellsCount = 0;
            yield return new WaitForSeconds(duration);

            Destroy(this);
        }

        private void OnDestroy() => _shield.CellsCount = _shieldCells;
    }
}