using System;
using SustainTheStrain.Buildings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Input
{
    public interface IBuildingPlaceholderInput : ISelectableInput<BuildingPlaceholder> {}

    public interface IHeroInput : ISelectableInput<Hero>
    {
        public event Action<Hero, RaycastHit> OnMove;
    }

    public interface IAbilityInput
    {
        public event Action<Ray> OnAbilityMove;
        public event Action<Ray> OnAbilityClick;
        public event Action<int> OnAbilityEnter;
        public event Action<int> OnAbilityChanged;
        public event Action<int> OnAbilityExit;
    }

    public interface ISelectableInput<out T>
    {
        public event Action<T> OnPointerEnter;
        public event Action<T> OnPointerExit;
        public event Action<T> OnSelected;
        public event Action<T> OnDeselected;
    }

    public interface IMouseMove
    {
        public event Action<RaycastHit> OnMouseMove;
    }
}