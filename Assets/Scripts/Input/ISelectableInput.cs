using System;
using UnityEngine;

namespace SustainTheStrain.Input
{
    public interface IMouseMove
    {
        public event Action<RaycastHit> OnMouseMove;
    }

    public interface ISelectableInput<out T> : IMouseMove
    {
        public event Action<T> OnPointerEnter;
        public event Action<T> OnPointerExit;
        public event Action<T> OnSelected;
        public event Action<T> OnDeselected;
    }

    public interface IAbilityInput
    {
        public event Action<Ray> OnAbilityMove;
        public event Action<Ray> OnAbilityClick;
        public event Action<int> OnAbilityEnter;
        public event Action<int> OnAbilityChanged;
        public event Action<int> OnAbilityExit;
    }
}