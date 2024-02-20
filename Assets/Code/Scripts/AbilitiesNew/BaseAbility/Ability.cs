using NTC.FiniteStateMachine;
using SustainTheStrain._Architecture;
using UnityEngine;

namespace SustainTheStrain.AbilitiesNew
{
    public abstract class Ability<TModel> : IState<AbilityController>, IController<TModel, AbilityView> where TModel : AbilityData
    {
        public abstract TModel Model { get; }
        public abstract AbilityView View { get; }
        public AbilityController Initializer { get; }

        protected Ability(AbilityController abilityController) => Initializer = abilityController;

        public virtual void OnEnter()
        {
            Model.Changed += View.Display;
            View.Display(Model);
        }

        public virtual void OnExit() => Model.Changed -= View.Display;

        public virtual void OnRun()
        {
            if (Initializer.CurrentEnergy == 0) return;
            Model.CurrentReload += Model.ReloadingSpeed * Time.deltaTime;
        }
    }
}