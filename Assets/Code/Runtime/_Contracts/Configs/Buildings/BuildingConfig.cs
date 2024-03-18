using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Buildings
{
    public abstract class BuildingConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public byte Level { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float Price { get; private set; }
        [field: SerializeField, Min(0)] public float Compensation { get; private set; }

        [field: Header("Stats")]
        [field: SerializeField, Min(0)] public float Radius { get; private set; } = 5.0f;
    }
}