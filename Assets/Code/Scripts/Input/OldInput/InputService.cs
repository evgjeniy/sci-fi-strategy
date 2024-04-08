using System;
using NTC.FiniteStateMachine;
using SustainTheStrain.Input.States;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace SustainTheStrain.Input
{
    public class InputService : IInitializable, IDisposable, IAbilityInput
    {
        private readonly global::InputActions _actions = new();

        private readonly MouseMoveState _mouseMoveState;
        private readonly AbilitySelectionState _abilitySelectionState;

        public StateMachine<InputService> StateMachine { get; private set; }

        public InputService()
        {
            _mouseMoveState = new MouseMoveState(this, _actions.Mouse);
            _abilitySelectionState = new AbilitySelectionState(this, _actions.Mouse);
        }

        public void Enable() => _actions.Enable();
        public void Disable() => _actions.Disable();

        public void Initialize()
        {
           _actions.Mouse.EnterAbilityState.performed += EnterAbilityState;
           _actions.Mouse.ExitState.performed += EnterMouseMoveState;

            StateMachine = new StateMachine<InputService>(_mouseMoveState, _abilitySelectionState)
            {
                TransitionsEnabled = false
            };

            StateMachine.SetState<MouseMoveState>();

            Enable();
        }

        private void EnterMouseMoveState(InputAction.CallbackContext context)
        {
            StateMachine.SetState<MouseMoveState>();
        }

        private void EnterAbilityState(InputAction.CallbackContext context)
        {
            _abilitySelectionState.CurrentAbilityIndex = (int)context.ReadValue<float>();
            StateMachine.SetState<AbilitySelectionState>();
        }

        public void Dispose()
        {
            _actions.Mouse.EnterAbilityState.performed -= EnterAbilityState;
            _actions.Mouse.ExitState.performed -= EnterMouseMoveState;

            StateMachine.CurrentState.OnExit();

            Disable();
        }

        #region Events

        event Action<int> IAbilityInput.OnAbilityEnter
        {
            add => _abilitySelectionState.OnAbilityEnter += value;
            remove => _abilitySelectionState.OnAbilityEnter -= value;
        }

        event Action<int> IAbilityInput.OnAbilityChanged
        {
            add => _abilitySelectionState.OnAbilityChanged += value;
            remove => _abilitySelectionState.OnAbilityChanged -= value;
        }

        event Action<int> IAbilityInput.OnAbilityExit
        {
            add => _abilitySelectionState.OnAbilityExit += value;
            remove => _abilitySelectionState.OnAbilityExit -= value;
        }

        event Action<Ray> IAbilityInput.OnAbilityMove
        {
            add => _abilitySelectionState.OnAbilityMove += value;
            remove => _abilitySelectionState.OnAbilityMove -= value;
        }

        event Action<Ray> IAbilityInput.OnAbilityClick
        {
            add => _abilitySelectionState.OnAbilityClick += value;
            remove => _abilitySelectionState.OnAbilityClick -= value;
        }

        #endregion
    }
}