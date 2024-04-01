using SustainTheStrain.Roads;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IPlaceholder
    {
        public Transform transform { get; }
        public Road Road { get; }
        
        public void SetBuilding(IBuilding building);
        public void DestroyBuilding();
    }
}