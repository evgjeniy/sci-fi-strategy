using System;
using SustainTheStrain.Buildings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class PlaceholderPointerState : MouseMoveState
    {
        private readonly Action<BuildingPlaceholder> _onEnterCallback;
        private readonly Action<BuildingPlaceholder> _onExitCallback;

        public PlaceholderPointerState(InputService initializer, InputActions.MouseActions mouseActions,
            Action<BuildingPlaceholder> onEnterCallback = null,
            Action<BuildingPlaceholder> onExitCallback = null) : base(initializer, mouseActions)
        {
            _onEnterCallback = onEnterCallback;
            _onExitCallback = onExitCallback;
        }

        public override void OnEnter()
        {
            _onEnterCallback?.Invoke(Initializer.CashedData.Placeholder);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            _onExitCallback?.Invoke(Initializer.CashedData.Placeholder);
        }

        protected override void MouseMove(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<BuildingPlaceholder>(out _)) { /* ignore, maybe Action<BuildingPlaceholder> OnPlaceholderPointerMove callback */ }
            else if (hit.collider.TryGetComponent<Hero>(out var hero))
            {
                Initializer.CashedData.Hero = hero;
                Initializer.StateMachine.SetState<HeroPointerState>();
            }
            else Initializer.StateMachine.SetState<MouseMoveState>();
        }

        protected override void LeftMouseButtonClick(RaycastHit hit)
        {
            Initializer.StateMachine.SetState<PlaceholderSelectionState>();
        }
    }
}