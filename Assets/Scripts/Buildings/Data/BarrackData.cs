using SustainTheStrain.Buildings.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.Data
{
    [CreateAssetMenu(menuName = "Static Data/Buildings/Barrack", fileName = "Barrack")]
    public class BarrackData : BaseBuildingData<Barrack, BarrackData.Stats>
    {
        [System.Serializable]
        public class Stats
        {
            [field: SerializeField, Min(0.01f)] public float UnitMaxHealth { get; private set; } = 100.0f;
        }
    }
}