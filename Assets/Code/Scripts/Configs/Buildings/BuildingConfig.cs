using UnityEngine;

namespace SustainTheStrain.Configs.Buildings
{
    public abstract class BuildingConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)] public byte Level { get; private set; } = 1;
        [field: SerializeField, Min(0)] public int Price { get; private set; }
        [field: SerializeField, Min(0)] public int Compensation { get; private set; }
        [field: SerializeField] public LayerMask Mask { get; private set; }

        [field: Header("Stats")]
        [field: SerializeField, Min(0)] public float Radius { get; private set; } = 5.0f;

        public abstract int NextLevelPrice { get; }
    }
}