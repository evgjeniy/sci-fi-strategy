using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class AttackRadiusCheck : MonoBehaviour
    {
        private List<Unit> _attackZoneUnits = new List<Unit>();

        public List<Unit> AttackZoneUnits => _attackZoneUnits;

        public event Action<Unit> OnUnitEnteredAttackZone;
        public event Action<Unit> OnUnitLeftAttackZone;

        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.TryGetComponent<Unit>(out var unit);

            if (unit == null) return;

            Debug.Log("On unit entered attack zone");
            _attackZoneUnits.Add(unit);
            OnUnitEnteredAttackZone?.Invoke(unit);
        }

        private void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < _attackZoneUnits.Count; i++)
            {
                if (other.gameObject == _attackZoneUnits[i].gameObject)
                {
                    OnUnitLeftAttackZone?.Invoke(_attackZoneUnits[i]);
                    _attackZoneUnits.RemoveAt(i);
                    Debug.Log("On unit left attack zone");
                    break;
                }
            }
        }
    }
}
