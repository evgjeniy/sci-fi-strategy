/*using System;
using SustainTheStrain.Buildings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class PlaceholderPointerState : MouseMoveState
    {
        public event Action<BuildingPlaceholder> OnPlaceholderEnter;
        public event Action<BuildingPlaceholder> OnPlaceholderExit;
        
        public PlaceholderPointerState(InputService initializer, InputActions.MouseActions mouseActions) : base(initializer, mouseActions) {}

        public override void OnEnter()
        {
            OnPlaceholderEnter?.Invoke(Initializer.CashedData.Placeholder);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            OnPlaceholderExit?.Invoke(Initializer.CashedData.Placeholder);
        }

        protected override void MouseMove(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<BuildingPlaceholder>(out _)) { /* ignore, maybe Action<BuildingPlaceholder> OnPlaceholderPointerMove callback #1# }
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
}*/