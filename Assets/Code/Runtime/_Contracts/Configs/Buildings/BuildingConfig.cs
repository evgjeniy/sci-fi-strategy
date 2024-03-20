using UnityEngine;

namespace SustainTheStrain._Contracts.Configs.Buildings
{
    public abstract class BuildingConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public int Level { get; private set; } = 1;
        [field: SerializeField, Min(0)] public int Price { get; private set; }
        [field: SerializeField, Min(0)] public int Compensation { get; private set; }

        [field: Header("Stats")]
        [field: SerializeField, Min(0)] public float Radius { get; private set; } = 5.0f;
    }
}