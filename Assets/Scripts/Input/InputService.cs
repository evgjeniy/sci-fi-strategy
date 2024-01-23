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
    public class InputService : IInitializable, IDisposable,
        ISelectableInput<BuildingPlaceholder>,
        ISelectableInput<Hero>
    {
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public EventSystem EventSystem { get; private set; }
            [field: SerializeField] public LayerMask RayCastMask { get; private set; } = 255;
            [field: SerializeField] public float MaxDistance { get; private set; } = float.MaxValue;
        }

        public class MouseMoveData
        {
            public Ray Ray;
            public RaycastHit Hit;
            public Vector2 MousePosition;
            public bool IsPointerUnderUI;
        }

        private readonly Settings _settings;
        private readonly InputActions _actions = new();

        public MouseMoveData Data { get; } = new();
        public StateMachine<InputService> StateMachine { get; private set; }

        public InputService(Settings settings) => _settings = settings;
        public void Enable() => _actions.Enable();
        public void Disable() => _actions.Disable();

        public void Initialize()
        {
            InitializeStateMachine();

            Enable();
            _actions.Mouse.MousePosition.performed += OnMouseMoved;
            _actions.Mouse.LeftButton.performed += OnLeftMouseButton;
        }

        public void Dispose()
        {
            _actions.Mouse.MousePosition.performed -= OnMouseMoved;
            _actions.Mouse.LeftButton.performed -= OnLeftMouseButton;
            Disable();
        }

        private void InitializeStateMachine()
        {
            StateMachine = new StateMachine<InputService>(InitializeStates());
            InitializeTransitions();
            StateMachine.SetState<MouseMoveState>();
        }

        private IState<InputService>[] InitializeStates() => new IState<InputService>[]
        {
            new MouseMoveState(this, OnMouseMove),
            new PointerState<BuildingPlaceholder>(GetPlaceholder, OnPlaceholderEnter, OnPlaceholderExit),
            new SelectionState<BuildingPlaceholder>(GetPlaceholder, OnPlaceholderSelected, OnPlaceholderDeselected),
            new PointerState<Hero>(GetHero, OnHeroEnter, OnHeroExit),
            new SelectionState<Hero>(GetHero, OnHeroSelected, OnHeroDeselected)
        };

        private void InitializeTransitions()
        {
            StateMachine.AddTransition<MouseMoveState, PointerState<BuildingPlaceholder>>(() => Data.Hit.IsPointerUnder(ref _placeholder));
            StateMachine.AddTransition<MouseMoveState, PointerState<Hero>>(() => Data.Hit.IsPointerUnder(ref _hero));
            
            StateMachine.AddTransition<PointerState<BuildingPlaceholder>, MouseMoveState>(() => !Data.Hit.IsPointerUnder(ref _placeholder));
            StateMachine.AddTransition<PointerState<BuildingPlaceholder>, SelectionState<BuildingPlaceholder>>(() => Mouse.current.leftButton.wasPressedThisFrame);
            
            StateMachine.AddTransition<PointerState<Hero>, MouseMoveState>(() => !Data.Hit.IsPointerUnder(ref _hero));
            StateMachine.AddTransition<PointerState<Hero>, SelectionState<Hero>>(() => Mouse.current.leftButton.wasPressedThisFrame);
            
            StateMachine.AddTransition<SelectionState<BuildingPlaceholder>, MouseMoveState>(() => Mouse.current.leftButton.wasPressedThisFrame && !Data.Hit.IsPointerUnder(ref _placeholder));
        }

        private void OnMouseMoved(InputAction.CallbackContext context)
        {
            Data.MousePosition = context.ReadValue<Vector2>();
            Data.IsPointerUnderUI = Data.MousePosition.IsPointerUnderUI(_settings.EventSystem);

            if (Data.IsPointerUnderUI) return;

            Data.Ray = Camera.main.ScreenPointToRay(Data.MousePosition);
            if (!Physics.Raycast(Data.Ray, out var hit, _settings.MaxDistance, _settings.RayCastMask)) return;

            Data.Hit = hit;
            StateMachine.Run();
        }

        private void OnLeftMouseButton(InputAction.CallbackContext _)
        {
            if (Data.IsPointerUnderUI) return;
            if (CheckIsPlaceholderSelectionState()) return;

            StateMachine.SetStateByTransitions();
        }

        private bool CheckIsPlaceholderSelectionState()
        {
            if (StateMachine.CurrentState is not SelectionState<BuildingPlaceholder>) return false;

            var placeholder = _placeholder;
            if (!Data.Hit.IsPointerUnder(ref _placeholder) || placeholder != _placeholder) return false;

            StateMachine.SetState<SelectionState<BuildingPlaceholder>>();
            return true;
        }

        #region Cashed Fields & Get Methods
        
        private Hero _hero;
        private BuildingPlaceholder _placeholder;
        
        private Hero GetHero() => _hero;
        private BuildingPlaceholder GetPlaceholder() => _placeholder;

        #endregion
        
        #region Events

        public event Action<RaycastHit> OnMouseMove;

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