using NTC.FiniteStateMachine;
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
                Initializer.Timer.Time -= Time.deltaTime;
                Initializer.Area.Update();

                if (!CheckTransitions()) return;
                OnOverridableRun();
            }

            protected virtual bool CheckTransitions()
            {
                if (Initializer.Area.Entities.Count != 0)
                {
                    Initializer.SetState<AttackState>();
                    return false;
                }

                return true;
            }

            protected virtual void OnOverridableRun() {}
        }
    }
}