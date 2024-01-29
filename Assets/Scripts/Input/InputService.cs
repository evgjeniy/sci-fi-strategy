﻿using System;
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
    public class InputService : IInitializable, IDisposable,
        ISelectableInput<BuildingPlaceholder>,
        ISelectableInput<Hero>,
        IAbilityInput
    {
        #region Nested Classes

        [Serializable]
        public class InputSettings
        {
            [field: SerializeField] public EventSystem EventSystem { get; private set; }
            [field: SerializeField] public LayerMask RayCastMask { get; private set; } = 255;
            [field: SerializeField] public float MaxDistance { get; private set; } = float.MaxValue;
        }

        public class InputData
        {
            public BuildingPlaceholder Placeholder;
            public Hero Hero;
        }

        #endregion

        private readonly InputActions _actions = new();
        private AbilitySelectionState _abilitySelectionState;

        public InputSettings Settings { get; }
        public InputData CashedData { get; } = new();
        public StateMachine<InputService> StateMachine { get; private set; }

        public InputService(InputSettings inputSettings) => Settings = inputSettings;
        public void Enable() => _actions.Enable();
        public void Disable() => _actions.Disable();

        public void Initialize()
        {
            _actions.Mouse.EnterAbilityState.performed += EnterAbilityState;
            _actions.Mouse.ExitState.performed += EnterMouseMoveState;
            
            _abilitySelectionState = new AbilitySelectionState(this, _actions.Mouse, OnAbilityMove, OnAbilityClick, OnAbilityChanged);

            StateMachine = new StateMachine<InputService>
            (
                new MouseMoveState(this, _actions.Mouse, OnMouseMove),
                new PlaceholderPointerState(this, _actions.Mouse, OnPlaceholderEnter, OnPlaceholderExit),
                new PlaceholderSelectionState(this, _actions.Mouse, OnPlaceholderSelected, OnPlaceholderDeselected),
                new HeroPointerState(this, _actions.Mouse, OnHeroEnter, OnHeroExit),
                new HeroSelectionState(this, _actions.Mouse, OnHeroSelected, OnHeroDeselected),
                _abilitySelectionState
            );

            StateMachine.SetState<MouseMoveState>();

            Enable();
        }

        private void EnterMouseMoveState(InputAction.CallbackContext obj) => StateMachine.SetState<MouseMoveState>();

        private void EnterAbilityState(InputAction.CallbackContext obj)
        {
            _abilitySelectionState.CurrentAbilityIndex = (int)obj.ReadValue<float>();
            StateMachine.SetState<AbilitySelectionState>();
        }

        public void Dispose()
        {
            _actions.Mouse.EnterAbilityState.performed -= EnterAbilityState;
            _actions.Mouse.ExitState.performed -= EnterMouseMoveState;
            
            Disable();
        }

        #region Events

        public event Action<RaycastHit> OnMouseMove;

        public event Action<int> OnAbilityChanged;
        public event Action<RaycastHit> OnAbilityMove;
        public event Action<RaycastHit> OnAbilityClick;

        public event Action<BuildingPlaceholder> OnPlaceholderEnter;
        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnPointerEnter
        {
            add => OnPlaceholderEnter += value;
            remove => OnPlaceholderEnter -= value;
        }

        public event Action<BuildingPlaceholder> OnPlaceholderExit;
        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnPointerExit
        {
            add => OnPlaceholderExit += value;
            remove => OnPlaceholderExit -= value;
        }

        public event Action<BuildingPlaceholder> OnPlaceholderSelected;
        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnSelected
        {
            add => OnPlaceholderSelected += value;
            remove => OnPlaceholderSelected -= value;
        }

        public event Action<BuildingPlaceholder> OnPlaceholderDeselected;
        event Action<BuildingPlaceholder> ISelectableInput<BuildingPlaceholder>.OnDeselected
        {
            add => OnPlaceholderDeselected += value;
            remove => OnPlaceholderDeselected -= value;
        }

        public event Action<Hero> OnHeroEnter;
        event Action<Hero> ISelectableInput<Hero>.OnPointerEnter
        {
            add => OnHeroEnter += value;
            remove => OnHeroEnter -= value;
        }

        public event Action<Hero> OnHeroExit;
        event Action<Hero> ISelectableInput<Hero>.OnPointerExit
        {
            add => OnHeroExit += value;
            remove => OnHeroExit -= value;
        }

        public event Action<Hero> OnHeroSelected;
        event Action<Hero> ISelectableInput<Hero>.OnSelected
        {
            add => OnHeroSelected += value;
            remove => OnHeroSelected -= value;
        }

        public event Action<Hero> OnHeroDeselected;
        event Action<Hero> ISelectableInput<Hero>.OnDeselected
        {
            add => OnHeroDeselected += value;
            remove => OnHeroDeselected -= value;
        }

        #endregion
    }
}