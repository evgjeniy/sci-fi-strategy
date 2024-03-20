using SustainTheStrain.Abilities;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain._Contracts.Buildings
{
    [RequireComponent(typeof(Outline))]
    public abstract class Building : MonoCashed<Outline>, IInputSelectable
    {
        public abstract void Upgrade();
        public abstract void Destroy();
        
        #region Implementation of IInputSelectable

        public virtual void OnPointerEnter() => Cashed1.Enable();
        public virtual void OnPointerExit() => Cashed1.Disable();
        public virtual void OnSelected() {}
        public virtual void OnDeselected() {}
        public virtual void OnSelectedLeftClick(Ray ray) {}
        public virtual void OnSelectedRightClick(Ray ray) {}
        public virtual void OnSelectedUpdate(Ray ray) {}
        
        #endregion
    }
}