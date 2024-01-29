using System;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class AbilitySelectionState : MouseMoveState
    {
        private readonly Action<int> _abilityChangedCallback;
        private int _currentAbilityIndex;
        
        public int CurrentAbilityIndex
        {
            get => _currentAbilityIndex;
            set
            {
                _currentAbilityIndex = value;
                _abilityChangedCallback?.Invoke(_currentAbilityIndex);
            }
        }

        public AbilitySelectionState(InputService initializer, InputActions.MouseActions mouseActions,
            Action<RaycastHit> mouseMoveCallback = null,
            Action<RaycastHit> leftMouseClickCallback = null,
            Action<int> abilityChangedCallback = null)
            : base(initializer, mouseActions, mouseMoveCallback, leftMouseClickCallback)
        {
            _abilityChangedCallback = abilityChangedCallback;
        }
        
        protected override void MouseMove(RaycastHit hit) => MouseMoveCallback?.Invoke(hit);

        protected override void LeftMouseButtonClick(RaycastHit hit) => LeftMouseClickCallback?.Invoke(hit);
    }
}