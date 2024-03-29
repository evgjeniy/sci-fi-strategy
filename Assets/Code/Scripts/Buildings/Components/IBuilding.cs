using SustainTheStrain.Input;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IBuilding : IInputSelectable
    {
        public Transform transform { get; }

        public void Upgrade();
        public void Destroy();
    }
}