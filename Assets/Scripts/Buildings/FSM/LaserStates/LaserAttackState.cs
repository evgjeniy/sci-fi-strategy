using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.LaserStates
{
    public partial class LaserStateMachine
    {
        private class AttackState : IdleState
        {
            public AttackState(LaserStateMachine initializer) : base(initializer) {}

            protected override bool CheckTransitions() => true;

            protected override void OnOverridableRun()
            {
                var target = GetTarget();
                if (target == null) { Initializer.SetState<IdleState>(); return; }

                RotateToTarget(target);
                if (IsLookingToTarget(target)) TryAttack(target);
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