using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

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

            AddUnit(unit);
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveUnit(other.gameObject);
        }

        private void AddUnit(Unit unit)
        {
            if (unit == null) return;

            Debug.Log(string.Format("[AttackRadius] On {0} entered {1} attack radius", unit.name, name));
            _attackZoneUnits.Add(unit);
            OnUnitEnteredAttackZone?.Invoke(unit);

            unit.Damageble.OnDied += UnitDied;
        }

        private void RemoveUnit(GameObject gameObject)
        {
            for (int i = 0; i < _attackZoneUnits.Count; i++)
            {
                if (gameObject == _attackZoneUnits[i].gameObject)
                {
                    OnUnitLeftAttackZone?.Invoke(_attackZoneUnits[i]);
                    _attackZoneUnits[i].Damageble.OnDied -= UnitDied;
                    Debug.Log(string.Format("[AttackRadius] On {0} left {1} attack radius", _attackZoneUnits[i].name, name));
                    _attackZoneUnits.RemoveAt(i);
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