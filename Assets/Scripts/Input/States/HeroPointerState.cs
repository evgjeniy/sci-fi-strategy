using System;
using SustainTheStrain.Buildings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class HeroPointerState : MouseMoveState
    {
        private readonly Action<Hero> _onEnterCallback;
        private readonly Action<Hero> _onExitCallback;

        public HeroPointerState(InputService initializer, InputActions.MouseActions mouseActions,
            Action<Hero> onEnterCallback = null, Action<Hero> onExitCallback = null) : base(initializer, mouseActions)
        {
            _onEnterCallback = onEnterCallback;
            _onExitCallback = onExitCallback;
        }

        public override void OnEnter()
        {
            _onEnterCallback?.Invoke(Initializer.CashedData.Hero);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            _onExitCallback?.Invoke(Initializer.CashedData.Hero);
        }

        protected override void MouseMove(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<Hero>(out _)) { /* ignore, maybe Action<Hero> OnHeroPointerMove callback */ }
            else if (hit.collider.TryGetComponent<BuildingPlaceholder>(out var placeholder))
            {
                Initializer.CashedData.Placeholder = placeholder;
                Initializer.StateMachine.SetState<PlaceholderPointerState>();
            }
            else Initializer.StateMachine.SetState<MouseMoveState>();
        }

        protected override void LeftMouseButtonClick(RaycastHit hit)
        {
            Initializer.StateMachine.SetState<HeroSelectionState>();
        }
    }
}