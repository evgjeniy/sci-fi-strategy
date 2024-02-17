using System.Linq;
using NTC.FiniteStateMachine;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.LaserStates
{
    public partial class LaserStateMachine
    {
        private class IdleState : IState<LaserStateMachine>
        {
            public LaserStateMachine Initializer { get; }
            public IdleState(LaserStateMachine initializer) => Initializer = initializer;

            public void OnRun()
            {
                Initializer.Timer.Time -= Time.deltaTime * Initializer.CooldownEnergyMultiplier;
                Initializer.Area.Update();

                if (!CheckTransitions()) return;

                OnOverridableRun();
            }

            protected virtual bool CheckTransitions()
            {
                if (GetTarget() != null)
                {
                    Initializer.SetState<AttackState>();
                    return false;
                }

                return true;
            }

            protected Collider GetTarget() => Initializer.Area.Entities
                .FirstOrDefault(e => e.TryGetComponent<Damageble>(out var d) && d.Team != 1);

            protected virtual void OnOverridableRun() {}
            public virtual void OnEnter() {}
            public virtual void OnExit() {}
        }
    }
}