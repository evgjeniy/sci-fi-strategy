using SustainTheStrain.Units.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Units
{
    public class AttackRadiusCheck : MonoBehaviour
    {
        private List<Duelable> _attackZoneUnits = new List<Duelable>();

        public List<Duelable> AttackZoneUnits => _attackZoneUnits;

        public event Action<Duelable> OnUnitEnteredAttackZone;
        public event Action<Duelable> OnUnitLeftAttackZone;

        private Area<IDamageable> _area;

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

            //Debug.Log(string.Format("[AttackRadius] On {0} entered {1} attack radius", unit.name, name));
            _attackZoneUnits.Add(unit);
            OnUnitEnteredAttackZone?.Invoke(unit);

            unit.Damageable.OnDied += UnitDied;
        }

        private void RemoveUnit(GameObject gameObject)
        {
            for (int i = 0; i < _attackZoneUnits.Count; i++)
            {
                if (gameObject == _attackZoneUnits[i].gameObject)
                {
                    OnUnitLeftAttackZone?.Invoke(_attackZoneUnits[i]);
                    _attackZoneUnits[i].Damageable.OnDied -= UnitDied;
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
