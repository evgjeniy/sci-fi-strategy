using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units.Components
{
    public class AggroRadiusCheck : MonoBehaviour
    {
        private List<Unit> _aggroZoneUnits = new List<Unit>();

        public List<Unit> AggroZoneUnits => _aggroZoneUnits;

        public event Action<Unit> OnUnitEnteredAggroZone;
        public event Action<Unit> OnUnitLeftAggroZone;

        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.TryGetComponent<Unit>(out var unit);

            if (unit == null) return;

            Debug.Log("On unit entered aggro zone");
            _aggroZoneUnits.Add(unit);
            OnUnitEnteredAggroZone?.Invoke(unit);
        }

        private void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < _aggroZoneUnits.Count; i++)
            {
                if (other.gameObject == _aggroZoneUnits[i].gameObject)
                {
                    OnUnitLeftAggroZone?.Invoke(_aggroZoneUnits[i]);
                    _aggroZoneUnits.RemoveAt(i);
                    Debug.Log("On unit left aggro zone");
                    break;
                }
            }
        }

        private void RemoveUnit(Unit unit)
        {
            _aggroZoneUnits.Remove(unit);
        }
    }
}

