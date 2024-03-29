using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IPlaceholder
    {
        public Transform transform { get; }
        public void SetBuilding(IBuilding building);
        public void DestroyBuilding();
    }
}