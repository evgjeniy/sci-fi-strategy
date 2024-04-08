using SustainTheStrain.Input;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IBuilding : IInputSelectable, IInputPointerable
    {
        public Transform transform { get; }

        public void Upgrade();
        public void Destroy();
    }
}