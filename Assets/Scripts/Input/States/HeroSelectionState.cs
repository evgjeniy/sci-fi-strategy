using System;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class HeroSelectionState : HeroPointerState
    {
        public HeroSelectionState(InputService initializer, InputActions.MouseActions mouseActions,
            Action<Hero> onSelectedCallback = null, Action<Hero> onDeselectedCallback = null)
            : base(initializer, mouseActions, onSelectedCallback, onDeselectedCallback) {}

        protected override void MouseMove(RaycastHit hit) {}

        protected override void LeftMouseButtonClick(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<Hero>(out var hero))
            {
                Initializer.CashedData.Hero = hero;
                Initializer.StateMachine.SetState<HeroSelectionState>();
            }
            else Initializer.StateMachine.SetState<MouseMoveState>();
        }
    }
}