using NTC.FiniteStateMachine;
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