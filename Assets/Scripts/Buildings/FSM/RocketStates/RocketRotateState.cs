using System.Linq;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.RocketStates
{
    public partial class RocketStateMachine
    {
        private class RotateState : IdleState
        {
            public RotateState(RocketStateMachine initializer) : base(initializer) {}

            protected override void CheckTransitions()
            {
                if (Initializer.Area.Entities.Count == 0) Initializer.SetState<IdleState>();
                else if (IsLookingToTarget()) Initializer.SetState<AttackState>();
            }

            protected override void OnOverridableRun()
            {
                Initializer.RocketTransform.rotation = Quaternion.Slerp
                (
                    Initializer.RocketTransform.rotation,
                    GetRotationToTarget(),
                    Time.deltaTime * 3.0f
                );
            }

            private Quaternion GetRotationToTarget()
            {
                var targetDirection = Initializer.Area.Entities.First().transform.position -
                                      Initializer.RocketTransform.position;
                return Quaternion.LookRotation(targetDirection, Initializer.RocketTransform.up);
            }

            protected bool IsLookingToTarget()
            {
                var target = Initializer.Area.Entities.First();
                var transform = Initializer.RocketTransform;

                return Vector3.Angle(transform.forward, target.transform.position - transform.position) < 1.0f;
            }
        }
    }
}