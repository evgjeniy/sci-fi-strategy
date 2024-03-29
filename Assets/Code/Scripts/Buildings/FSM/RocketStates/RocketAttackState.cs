using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class RocketStateMachine
    {
        private class AttackState : IdleState
        {
            public AttackState(RocketStateMachine initializer) : base(initializer) {}

            protected override bool CheckTransitions() => true;

            protected override void OnOverridableRun()
            {
                var target = GetTarget();
                if (target == null) { Initializer.SetState<IdleState>(); return; }

                RotateToTarget(target);
                if (IsLookingToTarget(target)) TryAttack();
            }

            private void RotateToTarget(Component target) => Initializer.RocketTransform.rotation = Quaternion.Slerp
            (
                Initializer.RocketTransform.rotation,
                GetRotationToTarget(target),
                Time.deltaTime * 10.0f
            );

            private Quaternion GetRotationToTarget(Component target)
            {
                var rocket = Initializer.RocketTransform;
                return Quaternion.LookRotation(target.transform.position - rocket.position, rocket.up);
            }

            private bool IsLookingToTarget(Component target)
            {
                var rocket = Initializer.RocketTransform;
                return Vector3.Angle(target.transform.position - rocket.position, rocket.forward) < 1.0f;
            }

            private void TryAttack()
            {
                if (!Initializer.Timer.IsTimeOver) return;

                var isAttackSuccess = false;
                var attackedAmount = 0;

                foreach (var collider in Initializer.Area.Entities)
                {
                    if (attackedAmount >= Initializer.CurrentStats.MaxEnemiesTargets) break;

                    if (!collider.TryGetComponent<Damageble>(out var damageable) || damageable.Team == 1) continue;
                    if (!IsInSector(damageable.transform)) continue;

                    var transform = Initializer.RocketTransform;
                    var projectile = Object.Instantiate(Initializer.ProjectilePrefab, transform.position, transform.rotation);
                    projectile.LaunchTo(damageable, d =>
                    {
                        d.Damage(Initializer.CurrentStats.Damage * Initializer.DamageEnergyMultiplier);
                    });

                    attackedAmount++;
                    isAttackSuccess = true;
                }

                if (isAttackSuccess)
                    Initializer.Timer.Time = Initializer.CurrentStats.AttackCooldown;
            }

            private bool IsInSector(Transform component)
            {
                var rocketTransform = Initializer.RocketTransform;
                var angle = Vector3.Angle(rocketTransform.forward, component.position - rocketTransform.position);

                return angle <= Initializer.CurrentStats.AttackSectorAngle * 0.5f;
            }
        }
    }
}