using System;
using UnityEngine;

namespace SustainTheStrain._Contracts
{
    public interface IInputSelectable
    {
        public void OnPointerEnter() {}
        public void OnPointerExit() {}
        public void OnSelected() {}
        public void OnDeselected() {}
        public void OnSelectedLeftClick(Ray ray) {}
        public void OnSelectedRightClick(Ray ray) {}
        public void OnSelectedUpdate(Ray ray) {}
    }

    public interface IInputSystem
    {
        public Vector2 MousePosition { get; }
        public InputSettings Settings { get; }
        public void Idle();
        public void Select(IInputSelectable selectable);
        public void Enable();
        public void Disable();
    }

    [Serializable]
    public class InputSettings
    {
        [field: SerializeField] public float Distance { get; private set; } = 100.0f;
        [field: SerializeField] public LayerMask Mask { get; private set; } = -1;
    }
}