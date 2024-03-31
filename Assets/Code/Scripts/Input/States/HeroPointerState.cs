using System;
using SustainTheStrain.Buildings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class HeroPointerState : MouseMoveState
    {
        public event Action<Hero> OnHeroEnter;
        public event Action<Hero> OnHeroExit;

        public HeroPointerState(InputService initializer, global::InputActions.MouseActions mouseActions) : base(initializer, mouseActions) {}

        public override void OnEnter()
        {
            OnHeroEnter?.Invoke(Initializer.CashedData.Hero);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            OnHeroExit?.Invoke(Initializer.CashedData.Hero);
        }

        protected override void MouseMove(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<Hero>(out _)) { /* ignore, maybe Action<Hero> OnHeroPointerMove callback */ }
            /*else if (hit.collider.TryGetComponent<BuildingPlaceholder>(out var placeholder))
            {
                Initializer.CashedData.Placeholder = placeholder;
                Initializer.StateMachine.SetState<PlaceholderPointerState>();
            }*/
            else Initializer.StateMachine.SetState<MouseMoveState>();
        }

        protected override void LeftMouseButtonClick(RaycastHit hit)
        {
            Initializer.StateMachine.SetState<HeroSelectionState>();
        }
    }
}