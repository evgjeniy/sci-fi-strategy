using UnityEngine;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IPlaceholder
    {
        public Transform transform { get; }
        public void SetBuilding(IBuilding building);
        public void DestroyBuilding();
    }
}