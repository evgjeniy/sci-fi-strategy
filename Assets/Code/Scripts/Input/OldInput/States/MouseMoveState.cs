using System;
using NTC.FiniteStateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SustainTheStrain.Input.States
{
    public class MouseMoveState : IState<InputService>
    {
        private readonly global::InputActions.MouseActions _mouseActions;

        public event Action<Ray> OnMouseMoveRay;
        public event Action<Ray> OnLeftMouseButtonClickRay;

        private Vector2 _mousePosition;

        public InputService Initializer { get; }

        public MouseMoveState(InputService initializer, global::InputActions.MouseActions mouseActions)
        {
            Initializer = initializer;
            _mouseActions = mouseActions;
        }

        public virtual void OnEnter()
        {
            _mouseActions.MousePosition.performed += MouseMovePerformed;
            _mouseActions.LeftButton.performed += MouseLeftButtonPerformed;
        }

        public virtual void OnExit()
        {
            _mouseActions.MousePosition.performed -= MouseMovePerformed;
            _mouseActions.LeftButton.performed -= MouseLeftButtonPerformed;
        }

        private void MouseMovePerformed(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
            
            if (_mousePosition.IsPointerUnderUI()) return;

            var ray = Camera.main.ScreenPointToRay(_mousePosition);
            OnMouseMoveRay?.Invoke(ray);
        }

        private void MouseLeftButtonPerformed(InputAction.CallbackContext context)
        {
            if (_mousePosition.IsPointerUnderUI()) return;
            
            var ray = Camera.main.ScreenPointToRay(_mousePosition);
            OnLeftMouseButtonClickRay?.Invoke(ray);
        }
    }
}