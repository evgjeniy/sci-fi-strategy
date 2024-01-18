using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace SustainTheStrain.Input
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        public Vector2 MousePosition { get; private set; }
        public event Action OnLeftMouseClick;
        public event Action OnMouseMove;
        
        private readonly InputActions _inputService;

        public InputService() => _inputService = new InputActions();

        public void Enable() => _inputService.Enable();

        public void Disable() => _inputService.Disable();

        public void Initialize()
        {
            Enable();
            _inputService.Mouse.MousePosition.performed += OnMouseMoved;
            _inputService.Mouse.LeftButton.performed += OnLeftMouseButton;
        }

        public void Dispose()
        {
            _inputService.Mouse.MousePosition.performed -= OnMouseMoved;
            _inputService.Mouse.LeftButton.performed -= OnLeftMouseButton;
            Disable();
        }

        private void OnMouseMoved(InputAction.CallbackContext obj)
        {
            MousePosition = obj.ReadValue<Vector2>();
            OnMouseMove?.Invoke();
        }

        private void OnLeftMouseButton(InputAction.CallbackContext obj)
        {
            OnLeftMouseClick?.Invoke();
        }
    }
}