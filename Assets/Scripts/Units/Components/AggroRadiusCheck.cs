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

            AddUnit(unit);
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveUnit(other.gameObject);
        }

        private void AddUnit(Unit unit)
        {
            if (unit == null) return;

            Debug.Log(string.Format("[AggroRadius] On {0} entered {1} aggro radius", unit.name, name));
            _aggroZoneUnits.Add(unit);
            OnUnitEnteredAggroZone?.Invoke(unit);

            unit.Damageble.OnDied += UnitDied;
        }

        private void RemoveUnit(GameObject gameObject)
        {
            for (int i = 0; i < _aggroZoneUnits.Count; i++)
            {
                if (gameObject == _aggroZoneUnits[i].gameObject)
                {
                    OnUnitLeftAggroZone?.Invoke(_aggroZoneUnits[i]);
                    _aggroZoneUnits[i].Damageble.OnDied -= UnitDied;
                    Debug.Log(string.Format("[AggroRadius] On {0} left {1} aggro radius", _aggroZoneUnits[i].name, name));
                    _aggroZoneUnits.RemoveAt(i);
                    break;
                }
            }
        }

        private void UnitDied(Damageble damageble)
        {
            RemoveUnit(damageble.gameObject);
        }
    }
}
