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
}