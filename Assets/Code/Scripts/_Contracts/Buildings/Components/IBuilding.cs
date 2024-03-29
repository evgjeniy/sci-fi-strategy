using UnityEngine;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuilding : IInputSelectable
    {
        public Transform transform { get; }

        public void Upgrade();
        public void Destroy();
    }
}