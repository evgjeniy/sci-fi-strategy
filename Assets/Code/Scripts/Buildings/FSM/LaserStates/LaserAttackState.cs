using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class LaserStateMachine
    {
        private class AttackState : IdleState
        {
            private readonly float _attackAngle = 25f;
            
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
                var euler = Quaternion.LookRotation(target.transform.position - laser.position, laser.up).eulerAngles;
                return Quaternion.Euler(Vector3.up * euler.y);
            }

            private bool IsLookingToTarget(Component target)
            {
                var laser = Initializer.LaserTransform;
                return Vector3.Angle(target.transform.position - laser.position, laser.forward) < _attackAngle;
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