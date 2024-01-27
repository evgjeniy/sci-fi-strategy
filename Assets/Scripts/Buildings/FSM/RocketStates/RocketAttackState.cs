using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.RocketStates
{
    public partial class RocketStateMachine
    {
        private class AttackState : RotateState
        {
            public AttackState(RocketStateMachine initializer) : base(initializer) {}

            protected override void CheckTransitions()
            {
                if (Initializer.Area.Entities.Count == 0) Initializer.SetState<IdleState>();
                else if (!IsLookingToTarget()) Initializer.SetState<RotateState>();
            }

            protected override void OnOverridableRun() => TryAttack();

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
                var direction = component.position - rocketTransform.position;
                var angle = Vector3.Angle(rocketTransform.forward, direction);

                return angle <= Initializer.CurrentStats.AttackSectorAngle * 0.5f;
            }
        }
    }
}