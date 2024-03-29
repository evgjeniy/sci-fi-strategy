using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Settings/UnitData")]
    public class UnitData : ScriptableObject
    {
        [field: SerializeField] public float MaxHP;
        [field: SerializeField] public float Damage;
        [field: SerializeField] public bool HasShield;
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float AttackCooldown;
    }
}
