using System.Collections;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class ShieldDeactivatorEffect : MonoBehaviour
    {
        private Coroutine _routine;
        private Shield _shield;

        public void Initialize(float duration)
        {
            if (TryGetComponent(out _shield) is false)
            {
                Destroy(this);
            }
            else
            {
                if (_routine != null) StopCoroutine(_routine);
                _routine = StartCoroutine(DeactivateShield(duration));
            }
        }

        private IEnumerator DeactivateShield(float duration)
        {
            _shield.CellsCount--;
            yield return new WaitForSeconds(duration);
            _shield.CellsCount++;
        }
    }
}