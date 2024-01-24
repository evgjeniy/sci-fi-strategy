using System;
using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SustainTheStrain.Input.States
{
    public class MouseMoveState : IState<InputService>
    {
        private readonly InputActions.MouseActions _mouseActions;
        private readonly Action<RaycastHit> _mouseMoveCallback;
        private readonly Action<RaycastHit> _leftMouseButtonClick;

        private Vector2 _mousePosition;

        public InputService Initializer { get; }

        public MouseMoveState(InputService initializer, InputActions.MouseActions mouseActions,
            Action<RaycastHit> mouseMoveCallback = null, Action<RaycastHit> leftMouseButtonClick = null)
        {
            Initializer = initializer;

            _mouseActions = mouseActions;
            _mouseMoveCallback = mouseMoveCallback;
            _leftMouseButtonClick = leftMouseButtonClick;
        }

        public virtual void OnEnter()
        {
            _mouseActions.MousePosition.performed += OnMouseMove;
            _mouseActions.LeftButton.performed += OnLeftButtonClicked;
        }

        public virtual void OnExit()
        {
            _mouseActions.MousePosition.performed -= OnMouseMove;
            _mouseActions.LeftButton.performed -= OnLeftButtonClicked;
        }

        private void OnMouseMove(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
            if (CheckScreenPointRayCastHit(out var hit)) MouseMove(hit);
        }

        private void OnLeftButtonClicked(InputAction.CallbackContext context)
        {
            if (CheckScreenPointRayCastHit(out var hit)) LeftMouseButtonClick(hit);
        }

        protected virtual void MouseMove(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<BuildingPlaceholder>(out var placeholder))
            {
                Initializer.CashedData.Placeholder = placeholder;
                Initializer.StateMachine.SetState<PlaceholderPointerState>();
            }
            else if (hit.collider.TryGetComponent<Hero>(out var hero))
            {
                Initializer.CashedData.Hero = hero;
                Initializer.StateMachine.SetState<HeroPointerState>();
            }
            else _mouseMoveCallback?.Invoke(hit);
        }

        protected virtual void LeftMouseButtonClick(RaycastHit hit) => _leftMouseButtonClick?.Invoke(hit);

        private bool CheckScreenPointRayCastHit(out RaycastHit hit)
        {
            hit = default;

            return !_mousePosition.IsPointerUnderUI(Initializer.Settings.EventSystem) && Physics.Raycast
            (
                Camera.main.ScreenPointToRay(_mousePosition), out hit,
                Initializer.Settings.MaxDistance,
                Initializer.Settings.RayCastMask.value
            );
        }
    }
}