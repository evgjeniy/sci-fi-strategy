using System;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Input.States
{
    public class HeroSelectionState : HeroPointerState
    {
        public event Action<Hero> OnHeroSelected;
        public event Action<Hero> OnHeroDeselected;
        public event Action<Hero, RaycastHit> OnHeroMove;
        
        public HeroSelectionState(InputService initializer, InputActions.MouseActions mouseActions) : base(initializer, mouseActions) {}
        
        public override void OnEnter()
        {
            OnHeroSelected?.Invoke(Initializer.CashedData.Hero);
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
            OnHeroDeselected?.Invoke(Initializer.CashedData.Hero);
        }
        
        protected override void MouseMove(RaycastHit hit) {}

        protected override void LeftMouseButtonClick(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent<Hero>(out var hero))
                Initializer.CashedData.Hero = hero;
            else
                OnHeroMove?.Invoke(Initializer.CashedData.Hero, hit);
        }
    }
}