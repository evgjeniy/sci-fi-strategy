using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    [CreateAssetMenu(fileName = "ShieldData", menuName = "Settings/ShieldData")]
    public class ShieldData : ScriptableObject
    {
        [field: SerializeField] public int CellsCount;
        [field: SerializeField] public float CellHp;
        [field: SerializeField] public float RecoverSpeed;
        [field: SerializeField] public float RecoverCooldown;
    }
}
