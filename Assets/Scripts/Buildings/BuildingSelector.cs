using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class BuildingSelector : MonoBehaviour
    {
        [Header("TEMP"), SerializeField] private RocketBuilding _rocketBuilding;

        public BaseBuilding GetSelectedBuilding() => _rocketBuilding;
    }
}