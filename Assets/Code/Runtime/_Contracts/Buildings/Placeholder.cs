using SustainTheStrain.Abilities;
using UnityEngine;
using UnityEngine.Extensions;
using Outline = SustainTheStrain.Abilities.Outline;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IPlaceholder
    {
        public Transform transform { get; }
        public void SetBuilding(Building building);
        public void DestroyBuilding();
    }
    
    [RequireComponent(typeof(Outline))]
    public class Placeholder : MonoCashed<Outline>, IInputSelectable, IPlaceholder
    {
        private Building _building;

        public void SetBuilding(Building building) {}
        public void DestroyBuilding() {}
        
        public void OnPointerEnter() => Cashed1.With(x => x.Enable());
        public void OnPointerExit() => Cashed1.With(x => x.Disable());
        public void OnSelected() => Cashed1.With(x => x.Enable()).With(x => x.OutlineColor = Color.red);
        public void OnDeselected() => Cashed1.With(x => x.Disable()).With(x => x.OutlineColor = Color.white);

        public void OnLeftClick(Ray ray) {}
    }
}