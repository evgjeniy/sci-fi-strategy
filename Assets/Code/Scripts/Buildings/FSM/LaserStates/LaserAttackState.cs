using SustainTheStrain.Units.Components;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.FSM.LaserStates
{
    public partial class LaserStateMachine
    {
        private class AttackState : IdleState
        {
            public AttackState(LaserStateMachine initializer) : base(initializer) {}

            protected override bool CheckTransitions() => true;

            public override void OnEnter() => Initializer.Context.Line.Enable();
            public override void OnExit() => Initializer.Context.Line.Disable();

            protected override void OnOverridableRun()
            {
                var target = GetTarget();
                if (target == null) { Initializer.SetState<IdleState>(); return; }

                RotateToTarget(target);
                UpdateLineRenderer(target);
                if (IsLookingToTarget(target)) TryAttack(target);
            }

            private void UpdateLineRenderer(Component target)
            {
                Initializer.Context.Line.SetPositions(new []
                {
                    Initializer.Context.transform.position,
                    target.transform.position
                });
            }

            private void RotateToTarget(Component target) => Initializer.LaserTransform.rotation = Quaternion.Slerp
            (
                Initializer.LaserTransform.rotation,
                GetRotationToTarget(target),
                Time.deltaTime * 3.0f
            );

            private Quaternion GetRotationToTarget(Component target)
            {
                var laser = Initializer.LaserTransform;
                return Quaternion.LookRotation(target.transform.position - laser.position, laser.up);
            }

            private bool IsLookingToTarget(Component target)
            {
                var laser = Initializer.LaserTransform;
                return Vector3.Angle(target.transform.position - laser.position, laser.forward) < 1.0f;
            }

            private void TryAttack(Component target)
            {
                if (!Initializer.Timer.IsTimeOver) return;
                if (!target.TryGetComponent<Damageble>(out var damageable) || damageable.Team == 1) return;

                damageable.Damage(Initializer.CurrentStats.Damage * Initializer.DamageEnergyMultiplier);
                Initializer.Timer.Time = Initializer.CurrentStats.AttackCooldown;
            }
        }
    }
}