using System;
using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class PlaceholderSelectionState : PlaceholderPointerState
    {
        public PlaceholderSelectionState(InputService initializer, InputActions.MouseActions mouseActions,
            Action<BuildingPlaceholder> onSelectedCallback = null,
            Action<BuildingPlaceholder> onDeselectedCallback = null)
            : base(initializer, mouseActions, onSelectedCallback, onDeselectedCallback) {}

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
}