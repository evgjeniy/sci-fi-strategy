using System;
using System.Linq;
using SustainTheStrain.Units.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SustainTheStrain.Buildings.FSM.ArtilleryStates
{
    public partial class ArtilleryStateMachine
    {
        private class AttackState : IdleState
        {
            private int _explodedSize;
            private readonly Collider[] _exploded = new Collider[16];

            public AttackState(ArtilleryStateMachine initializer) : base(initializer) {}

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

            private void RotateToTarget() => Initializer.ArtilleryTransform.rotation = Quaternion.Slerp
            (
                Initializer.ArtilleryTransform.rotation,
                GetRotationToTarget(),
                Time.deltaTime * 3.0f
            );

            private Quaternion GetRotationToTarget()
            {
                var artillery = Initializer.ArtilleryTransform;
                var targetPosition = Initializer.Area.Entities.First().transform.position;

                return Quaternion.LookRotation(targetPosition - artillery.position, artillery.up);
            }

            private bool IsLookingToTarget()
            {
                var artillery = Initializer.ArtilleryTransform;
                var targetPosition = Initializer.Area.Entities.First().transform.position;

                return Vector3.Angle(targetPosition - artillery.position, artillery.forward) < 1.0f;
            }

            private void TryAttack()
            {
                if (!Initializer.Timer.IsTimeOver) return;

                foreach (var entity in Initializer.Area.Entities)
                {
                    if (!entity.TryGetComponent<Damageble>(out var damageable)) continue;
                    if (damageable.Team == 1) continue;

                    Object.Instantiate
                    (
                        Initializer.ProjectilePrefab,
                        Initializer.ArtilleryTransform.position,
                        Initializer.ArtilleryTransform.rotation
                    ).LaunchTo(damageable, Explosion);

                    Initializer.Timer.Time = Initializer.CurrentStats.AttackCooldown;
                    break;
                }
            }

            private void Explosion(Damageble target)
            {
                Array.Clear(_exploded, 0, _explodedSize);
                _explodedSize = Physics.OverlapSphereNonAlloc(target.transform.position, Initializer.CurrentStats.ExplosionRadius, _exploded);

                for (var i = 0; i < _explodedSize; i++)
                {
                    if (!_exploded[i].TryGetComponent<Damageble>(out var damageable)) continue;
                    if (damageable.Team == 1) continue;
                    
                    damageable.Damage(Initializer.CurrentStats.Damage);
                }
            }
        }
    }
}