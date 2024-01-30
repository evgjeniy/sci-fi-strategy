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

        public event Action<Ray> OnMouseMoveRay;
        public event Action<Ray> OnLeftMouseButtonClickRay;
        public event Action<RaycastHit> OnMouseMove;
        public event Action<RaycastHit> OnLeftMouseButtonClick;

        private Vector2 _mousePosition;

        public InputService Initializer { get; }

        public MouseMoveState(InputService initializer, InputActions.MouseActions mouseActions)
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
            
            if (_mousePosition.IsPointerUnderUI(Initializer.Settings.EventSystem)) return;

            var ray = Camera.main.ScreenPointToRay(_mousePosition);
            OnMouseMoveRay?.Invoke(ray);
            
            if (CheckScreenPointRayCastHit(ray, out var hit)) MouseMove(hit);
        }

        private void MouseLeftButtonPerformed(InputAction.CallbackContext context)
        {
            if (_mousePosition.IsPointerUnderUI(Initializer.Settings.EventSystem)) return;
            
            var ray = Camera.main.ScreenPointToRay(_mousePosition);
            OnLeftMouseButtonClickRay?.Invoke(ray);
            
            if (CheckScreenPointRayCastHit(ray, out var hit)) LeftMouseButtonClick(hit);
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
            else OnMouseMove?.Invoke(hit);
        }

        protected virtual void LeftMouseButtonClick(RaycastHit hit) => OnLeftMouseButtonClick?.Invoke(hit);

        private bool CheckScreenPointRayCastHit(Ray ray, out RaycastHit hit) => Physics.Raycast
        (
            ray, out hit, Initializer.Settings.MaxDistance, Initializer.Settings.RayCastMask.value
        );
    }
}