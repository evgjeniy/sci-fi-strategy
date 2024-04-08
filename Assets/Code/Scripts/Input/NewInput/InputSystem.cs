namespace SustainTheStrain.Input
{
    public class InputSystem : IInputSystem, Zenject.IInitializable, Zenject.ITickable, System.IDisposable
    {
        private readonly InputActions _inputActions = new();
        private readonly InputData _inputData;

        private IInputState _currentState = new InputIdleState();

        public InputSystem(InputData inputData) => _inputData = inputData;

        #region Implementation of IInputSystem

        public IInputData InputData => _inputData;
        public void Idle() => ChangeState(_ => new InputIdleState());
        public void Select(IInputSelectable selectable) => ChangeState(_ => new InputSelectState(selectable));
        public void Enable() => _inputActions.Enable();
        public void Disable() => _inputActions.Disable();

        #endregion

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
            if (_inputData.MousePosition.IsPointerUnderUI()) return;
            ChangeState(_currentState.ProcessFrame);
        }

        private void PerformMouseMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _inputData.MousePosition = context.ReadValue<UnityEngine.Vector2>();
        }

        private void PerformLeftClick(UnityEngine.InputSystem.InputAction.CallbackContext _)
        {
            if (_inputData.MousePosition.IsPointerUnderUI()) return;
            ChangeState(_currentState.ProcessLeftClick);
        }

        private void PerformRightClick(UnityEngine.InputSystem.InputAction.CallbackContext _)
        {
            if (_inputData.MousePosition.IsPointerUnderUI()) return;
            ChangeState(_currentState.ProcessRightClick);
        }

        private void ChangeState(System.Func<IInputData, IInputState> getNewState)
        {
            var newState = getNewState(_inputData);
            if (Equals(newState, _currentState)) return;

            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}