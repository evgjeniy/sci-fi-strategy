using System;
using UnityEngine;

namespace SustainTheStrain.Input
{
    public interface IInputService
    {
        public Vector2 MousePosition { get; }
        public event Action OnLeftMouseClick;
        public event Action OnMouseMove;
    }
}