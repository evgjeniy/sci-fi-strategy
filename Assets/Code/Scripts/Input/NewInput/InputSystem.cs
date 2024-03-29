using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace SustainTheStrain.Input
{
    public class InputSystem : IInputSystem, IInitializable, ITickable, IDisposable
    {
        #region Implementation of IInputSystem

        public Vector2 MousePosition { get; private set; }
        public InputSettings Settings { get; }

        public void Idle() => ChangeState(_ => new IdleState());
        public void Select(IInputSelectable selectable) => ChangeState(_ => new SelectableState(selectable));
        public void Enable() => _inputActions.Enable();
        public void Disable() => _inputActions.Disable();

        #endregion

        private readonly InputActions _inputActions = new();

        private IInputState _currentState = new IdleState();

        public InputSystem(InputSettings inputSettings) => Settings = inputSettings;

        public void Initialize()
        {
            _currentState.Enter();

            _inputActions.Mouse.Position.performed += PerformMouseMove;
            _inputActions.Mouse.LeftClick.performed += PerformLeftClick;
            _inputActions.Mouse.RightClick.performed += PerformRightClick;

            Enable();
        }

        public void Dispose()
        {
            Disable();

            _inputActions.Mouse.Position.performed -= PerformMouseMove;
            _inputActions.Mouse.LeftClick.performed -= PerformLeftClick;
            _inputActions.Mouse.RightClick.performed -= PerformRightClick;

            _currentState.Exit();
        }

        public void Tick()
        {
            if (MousePosition.IsPointerUnderUI()) return;
            ChangeState(_currentState.ProcessMouseMove);
        }

        private void PerformMouseMove(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        private void PerformLeftClick(InputAction.CallbackContext _)
        {
            if (MousePosition.IsPointerUnderUI()) return;
            ChangeState(_currentState.ProcessLeftClick);
        }

        private void PerformRightClick(InputAction.CallbackContext _)
        {
            if (MousePosition.IsPointerUnderUI()) return;
            ChangeState(_currentState.ProcessRightClick);
        }

        private void ChangeState(Func<IInputSystem, IInputState> getNewState)
        {
            var newState = getNewState(this);
            if (Equals(newState, _currentState)) return;

            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}