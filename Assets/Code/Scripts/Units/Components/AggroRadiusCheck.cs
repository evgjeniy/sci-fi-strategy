using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units
{
    public class AggroRadiusCheck : MonoBehaviour
    {
        private List<Duelable> _aggroZoneUnits = new List<Duelable>();

        public List<Duelable> AggroZoneUnits => _aggroZoneUnits;

        public event Action<Duelable> OnUnitEnteredAggroZone;
        public event Action<Duelable> OnUnitLeftAggroZone;

        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.TryGetComponent<Duelable>(out var unit);

            AddUnit(unit);
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveUnit(other.gameObject);
        }

        private void AddUnit(Duelable unit)
        {
            if (unit == null) return;

            Debug.Log(string.Format("[AggroRadius] On {0} entered {1} aggro radius", unit.name, name));
            _aggroZoneUnits.Add(unit);
            OnUnitEnteredAggroZone?.Invoke(unit);

            unit.Damageable.OnDied += UnitDied;
        }

        private void RemoveUnit(GameObject gameObject)
        {
            for (int i = 0; i < _aggroZoneUnits.Count; i++)
            {
                if (gameObject == _aggroZoneUnits[i].gameObject)
                {
                    OnUnitLeftAggroZone?.Invoke(_aggroZoneUnits[i]);
                    _aggroZoneUnits[i].Damageable.OnDied -= UnitDied;
                    //Debug.Log(string.Format("[AggroRadius] On {0} left {1} aggro radius", _aggroZoneUnits[i].name, name));
                    _aggroZoneUnits.RemoveAt(i);
                    break;
                }
            }
        }

        private void UnitDied(Damageble damageble)
        {
            if (damageble == null) return;
            RemoveUnit(damageble.gameObject);
        }
    }
}

