using System.Linq;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.RocketStates
{
    public partial class RocketStateMachine
    {
        private class AttackState : IdleState
        {
            public AttackState(RocketStateMachine initializer) : base(initializer) {}

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

            private void RotateToTarget() => Initializer.RocketTransform.rotation = Quaternion.Slerp
            (
                Initializer.RocketTransform.rotation,
                GetRotationToTarget(),
                Time.deltaTime * 3.0f
            );

            private Quaternion GetRotationToTarget()
            {
                var rocket = Initializer.RocketTransform;
                var targetPosition = Initializer.Area.Entities.First().transform.position;

                return Quaternion.LookRotation(targetPosition - rocket.position, rocket.up);
            }

            private bool IsLookingToTarget()
            {
                var rocket = Initializer.RocketTransform;
                var targetPosition = Initializer.Area.Entities.First().transform.position;

                return Vector3.Angle(targetPosition - rocket.position, rocket.forward) < 1.0f;
            }

            private void TryAttack()
            {
                if (!Initializer.Timer.IsTimeOver) return;

                var isAttackSuccess = false;
                var attackedAmount = 0;

                foreach (var collider in Initializer.Area.Entities)
                {
                    if (attackedAmount >= Initializer.CurrentStats.MaxEnemiesTargets) break;

                    if (!collider.TryGetComponent<Damageble>(out var damageable)) continue;
                    if (!IsInSector(damageable.transform)) continue;

                    var transform = Initializer.RocketTransform;
                    var projectile = Object.Instantiate(Initializer.ProjectilePrefab, transform.position, transform.rotation);
                    projectile.LaunchTo(damageable, d => d.Damage(Initializer.CurrentStats.Damage));

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