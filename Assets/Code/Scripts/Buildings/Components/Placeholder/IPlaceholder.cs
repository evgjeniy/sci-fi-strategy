using SustainTheStrain.Input;
using SustainTheStrain.Roads;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IPlaceholder : ITransformable, IInputSelectable, IInputPointerable
    {
        public Road Road { get; }
        
        public void SetBuilding(IBuilding building);
        public void DestroyBuilding();
    }
}