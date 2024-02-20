using NTC.FiniteStateMachine;
using SustainTheStrain._Architecture;
using UnityEngine;

namespace SustainTheStrain.AbilitiesNew
{
    public abstract class Ability<TModel, TView> : IState<AbilityController>, IController<TModel, TView>
        where TModel : AbilityData, IModel<TModel>
        where TView : AbilityView<TModel>
    {
        public abstract TModel Model { get; }
        public abstract TView View { get; }
        public AbilityController Initializer { get; }

        protected Ability(AbilityController abilityController) => Initializer = abilityController;

        public virtual void OnEnter() => Model.Changed += View.Display;

        public virtual void OnExit() => Model.Changed -= View.Display;

        public virtual void OnRun()
        {
            if (Initializer.CurrentEnergy == 0) return;
            Model.CurrentReload += Model.ReloadingSpeed * Time.deltaTime;
        }
    }
}