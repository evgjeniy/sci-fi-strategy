using System.Linq;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.LaserStates
{
    public partial class LaserStateMachine
    {
        private class AttackState : IdleState
        {
            public AttackState(LaserStateMachine initializer) : base(initializer) {}

            protected override bool CheckTransitions()
            {
                if (Initializer.Area.Entities.Count == 0)
                {
                    Initializer.SetState<IdleState>();
                    return false;
                }

                return true;
            }

            protected override void OnOverridableRun()
            {
                RotateToTarget();

                if (IsLookingToTarget())
                    TryAttack();
            }

            private void RotateToTarget() => Initializer.LaserTransform.rotation = Quaternion.Slerp
            (
                Initializer.LaserTransform.rotation,
                GetRotationToTarget(),
                Time.deltaTime * 3.0f
            );

            private Quaternion GetRotationToTarget()
            {
                var targetDirection = Initializer.Area.Entities.First().transform.position -
                                      Initializer.LaserTransform.position;
                return Quaternion.LookRotation(targetDirection, Initializer.LaserTransform.up);
            }

            private bool IsLookingToTarget()
            {
                var target = Initializer.Area.Entities.First();
                var transform = Initializer.LaserTransform;

                return Vector3.Angle(transform.forward, target.transform.position - transform.position) < 1.0f;
            }

            private void TryAttack()
            {
                if (!Initializer.Timer.IsTimeOver) return;

                var collider = Initializer.Area.Entities.First();
                if (!collider.TryGetComponent<Damageble>(out var damageable)) return;

                damageable.Damage(Initializer.CurrentStats.Damage);

                Initializer.Timer.Time = Initializer.CurrentStats.AttackCooldown;
            }
        }
    }
}