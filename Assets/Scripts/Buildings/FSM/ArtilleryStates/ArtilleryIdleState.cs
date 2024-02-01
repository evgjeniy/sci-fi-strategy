using NTC.FiniteStateMachine;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.ArtilleryStates
{
    public partial class ArtilleryStateMachine
    {
        private class IdleState : IState<ArtilleryStateMachine>
        {
            public ArtilleryStateMachine Initializer { get; }
            public IdleState(ArtilleryStateMachine initializer) => Initializer = initializer;

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