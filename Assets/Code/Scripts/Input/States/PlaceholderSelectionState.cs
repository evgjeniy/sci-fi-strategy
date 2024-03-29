/*using System;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class PlaceholderSelectionState : PlaceholderPointerState
    {
        public event Action<BuildingPlaceholder> OnPlaceholderSelected;
        public event Action<BuildingPlaceholder> OnPlaceholderDeselected;
        
        public PlaceholderSelectionState(InputService initializer, InputActions.MouseActions mouseActions) : base(initializer, mouseActions) {}

        public override void OnEnter()
        {
            OnPlaceholderSelected?.Invoke(Initializer.CashedData.Placeholder);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            OnPlaceholderDeselected?.Invoke(Initializer.CashedData.Placeholder);
        }

        protected override void MouseMove(RaycastHit hit) {}
        
        protected override void LeftMouseButtonClick(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<BuildingPlaceholder>(out var placeholder))
            {
                Initializer.CashedData.Placeholder = placeholder;
                Initializer.StateMachine.SetState<PlaceholderSelectionState>();
            }
            else Initializer.StateMachine.SetState<MouseMoveState>();
        }
    }
}*/