using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRadiusCheck : MonoBehaviour
{
    private List<Unit> _aggroZoneUnits = new List<Unit>();

    public List<Unit> AggroZoneUnits => _aggroZoneUnits;

    public event Action<Unit> onUnitEnteredAgroZone;
    public event Action<Unit> onUnitLeftAgroZone;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<Unit>(out var unit);

        if (unit == null) return;

        Debug.Log("On unit entered aggro zone");
        _aggroZoneUnits.Add(unit);
        onUnitEnteredAgroZone?.Invoke(unit);
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < _aggroZoneUnits.Count; i++)
        {
            if (other.gameObject == _aggroZoneUnits[i].gameObject)
            {
                onUnitLeftAgroZone?.Invoke(_aggroZoneUnits[i]);
                _aggroZoneUnits.RemoveAt(i);
                Debug.Log("On unit left aggro zone");
                break;
            }
        }
    }
}

