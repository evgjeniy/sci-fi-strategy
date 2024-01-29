﻿using System;
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
        protected readonly Action<RaycastHit> MouseMoveCallback;
        protected readonly Action<RaycastHit> LeftMouseClickCallback;

        private Vector2 _mousePosition;

        public InputService Initializer { get; }

        public MouseMoveState(InputService initializer, InputActions.MouseActions mouseActions,
            Action<RaycastHit> mouseMoveCallback = null, Action<RaycastHit> leftMouseClickCallback = null)
        {
            Initializer = initializer;

            _mouseActions = mouseActions;
            MouseMoveCallback = mouseMoveCallback;
            LeftMouseClickCallback = leftMouseClickCallback;
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
            else MouseMoveCallback?.Invoke(hit);
        }

        protected virtual void LeftMouseButtonClick(RaycastHit hit) => LeftMouseClickCallback?.Invoke(hit);

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