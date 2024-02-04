using System.Linq;
using NTC.FiniteStateMachine;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.RocketStates
{
    public partial class RocketStateMachine
    {
        private class IdleState : IState<RocketStateMachine>
        {
            public RocketStateMachine Initializer { get; }
            public IdleState(RocketStateMachine initializer) => Initializer = initializer;

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
        }
    }
}