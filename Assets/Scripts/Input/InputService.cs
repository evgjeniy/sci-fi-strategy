using System;
using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings;
using SustainTheStrain.Input.States;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace SustainTheStrain.Input
{
    public class InputService : IInitializable, IDisposable, IMouseMove, IBuildingPlaceholderInput, IHeroInput, IAbilityInput
    {
        #region Nested Classes

        [Serializable]
        public class InputSettings
        {
            [field: SerializeField] public EventSystem EventSystem { get; private set; }
            [field: SerializeField] public LayerMask RayCastMask { get; private set; } = 255;
            [field: SerializeField] public float MaxDistance { get; private set; } = float.MaxValue;
            [field: SerializeField] public LayerMask AbilityMask { get; private set; } = 255;
        }

        public class InputData
        {
            public BuildingPlaceholder Placeholder;
            public Hero Hero;
        }

        #endregion

        private readonly InputActions _actions = new();

        private readonly MouseMoveState _mouseMoveState;
        private readonly PlaceholderPointerState _placeholderPointerState;
        private readonly PlaceholderSelectionState _placeholderSelectionState;
        private readonly HeroPointerState _heroPointerState;
        private readonly HeroSelectionState _heroSelectionState;
        private readonly AbilitySelectionState _abilitySelectionState;

        public InputSettings Settings { get; }
        public InputData CashedData { get; } = new();
        public StateMachine<InputService> StateMachine { get; private set; }

        public InputService(InputSettings inputSettings)
        {
            Settings = inputSettings;

            _mouseMoveState = new MouseMoveState(this, _actions.Mouse);
            _placeholderPointerState = new PlaceholderPointerState(this, _actions.Mouse);
            _placeholderSelectionState = new PlaceholderSelectionState(this, _actions.Mouse);
            _heroPointerState = new HeroPointerState(this, _actions.Mouse);
            _heroSelectionState = new HeroSelectionState(this, _actions.Mouse);
            _abilitySelectionState = new AbilitySelectionState(this, _actions.Mouse);
        }

        public void Enable() => _actions.Enable();
        public void Disable() => _actions.Disable();

        public void Initialize()
        {
            _actions.Mouse.EnterAbilityState.performed += EnterAbilityState;
            _actions.Mouse.ExitState.performed += EnterMouseMoveState;

            StateMachine = new StateMachine<InputService>
            (
                _mouseMoveState, _placeholderPointerState, _placeholderSelectionState,
                _heroPointerState, _heroSelectionState, _abilitySelectionState
            );

            StateMachine.SetState<MouseMoveState>();

            Enable();
        }

        private void EnterMouseMoveState(InputAction.CallbackContext context) =>
            StateMachine.SetState<MouseMoveState>();

        private void EnterAbilityState(InputAction.CallbackContext context) =>
            _abilitySelectionState.CurrentAbilityIndex = (int)context.ReadValue<float>();

        public void Dispose()
        {
            _actions.Mouse.EnterAbilityState.performed -= EnterAbilityState;
            _actions.Mouse.ExitState.performed -= EnterMouseMoveState;

            StateMachine.CurrentState.OnExit();

            Disable();
        }

        #region Events

        event Action<RaycastHit> IMouseMove.OnMouseMove
        {
            add => _mouseMoveState.OnMouseMove += value;
            remove => _mouseMoveState.OnMouseMove -= value;
        }

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

        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnPointerEnter
        {
            add => _placeholderPointerState.OnPlaceholderEnter += value;
            remove => _placeholderPointerState.OnPlaceholderEnter -= value;
        }

        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnPointerExit
        {
            add => _placeholderPointerState.OnPlaceholderExit += value;
            remove => _placeholderPointerState.OnPlaceholderExit -= value;
        }

        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnSelected
        {
            add => _placeholderSelectionState.OnPlaceholderSelected += value;
            remove => _placeholderSelectionState.OnPlaceholderSelected -= value;
        }

        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnDeselected
        {
            add => _placeholderSelectionState.OnPlaceholderDeselected += value;
            remove => _placeholderSelectionState.OnPlaceholderDeselected -= value;
        }

        event Action<Hero> ISelectableInput<Hero>.OnPointerEnter
        {
            add => _heroPointerState.OnHeroEnter += value;
            remove => _heroPointerState.OnHeroEnter -= value;
        }

        event Action<Hero> ISelectableInput<Hero>.OnPointerExit
        {
            add => _heroPointerState.OnHeroExit += value;
            remove => _heroPointerState.OnHeroExit -= value;
        }

        event Action<Hero> ISelectableInput<Hero>.OnSelected
        {
            add => _heroSelectionState.OnHeroSelected += value;
            remove => _heroSelectionState.OnHeroSelected -= value;
        }

        event Action<Hero> ISelectableInput<Hero>.OnDeselected
        {
            add => _heroSelectionState.OnHeroDeselected += value;
            remove => _heroSelectionState.OnHeroDeselected -= value;
        }

        event Action<Hero, RaycastHit> IHeroInput.OnMove
        {
            add => _heroSelectionState.OnHeroMove += value;
            remove => _heroSelectionState.OnHeroMove -= value;
        }

        #endregion
    }
}