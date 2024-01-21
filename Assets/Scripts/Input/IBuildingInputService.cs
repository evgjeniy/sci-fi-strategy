using System;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Input
{
    public interface IInputService
    {
        public event Action<Vector2> OnLeftMouseClick;
    }

    public interface IBuildingInputService : IInputService
    {
        public event Action<BuildingPlaceholder> OnPlaceholderPointerEnter;
        public event Action<BuildingPlaceholder> OnPlaceholderPointerExit;
        public event Action<BuildingPlaceholder> OnPlaceholderPointerLeftClick;
    }
}