using System;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class AbilitySelectionState : MouseMoveState
    {
        public event Action<Ray> OnAbilityMove
        {
            add => OnMouseMoveRay += value;
            remove => OnMouseMoveRay -= value;
        }

        protected override void MouseMove(RaycastHit hit)
        {
            
        }

        public event Action<Ray> OnAbilityClick
        {
            add => OnLeftMouseButtonClickRay += value;
            remove => OnLeftMouseButtonClickRay -= value;
        }

        public event Action<int> OnAbilityEnter;
        public event Action<int> OnAbilityChanged;
        public event Action<int> OnAbilityExit;
        
        
        private int _currentAbilityIndex;
        
        public int CurrentAbilityIndex
        {
            get => _currentAbilityIndex;
            set
            {
                if (Initializer.StateMachine.CurrentState is not AbilitySelectionState)
                {
                    _currentAbilityIndex = value;
                    Initializer.StateMachine.SetState<AbilitySelectionState>();
                    return;
                }

                if (_currentAbilityIndex == value)
                    Initializer.StateMachine.SetState<MouseMoveState>();
                else
                    OnAbilityChanged?.Invoke(_currentAbilityIndex = value);
            }
        }

        public AbilitySelectionState(InputService initializer,
            global::InputActions.MouseActions mouseActions) : base(initializer, mouseActions) {}

        public override void OnEnter()
        {
            OnAbilityEnter?.Invoke(_currentAbilityIndex);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            OnAbilityExit?.Invoke(_currentAbilityIndex);
        }
    }
}