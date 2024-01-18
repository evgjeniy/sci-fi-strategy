using SustainTheStrain.Buildings.Data;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class BuildingSelector : MonoBehaviour
    {
        [Header("TEMP"), SerializeField] private RocketData _rocketBuilding;
        
        public RocketData SelectedBuilding => _rocketBuilding;
    }
}