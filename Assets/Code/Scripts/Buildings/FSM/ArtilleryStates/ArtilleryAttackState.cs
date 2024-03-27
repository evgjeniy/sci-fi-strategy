using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class ArtilleryStateMachine
    {
        private class AttackState : IdleState
        {
            private int _explodedSize;
            private readonly Collider[] _exploded = new Collider[32];

            private readonly float _attackAngle = 25f;
            
            public AttackState(ArtilleryStateMachine initializer) : base(initializer) {}

            protected override bool CheckTransitions() => true;

            protected override void OnOverridableRun()
            {
                var target = GetTarget();
                if (target == null) { Initializer.SetState<IdleState>(); return; }

                RotateToTarget(target);
                if (IsLookingToTarget(target)) TryAttack(target);
            }

            private void RotateToTarget(Component target) => Initializer.ArtilleryTransform.rotation = Quaternion.Slerp
            (
                Initializer.ArtilleryTransform.rotation,
                GetRotationToTarget(target),
                Time.deltaTime * 3.0f
            );

            private Quaternion GetRotationToTarget(Component target)
            {
                var artillery = Initializer.ArtilleryTransform;
                var euler = Quaternion.LookRotation(target.transform.position - artillery.position, artillery.up).eulerAngles;
                return Quaternion.Euler(Vector3.up * euler.y);
            }

            private bool IsLookingToTarget(Component target)
            {
                var artillery = Initializer.ArtilleryTransform;
                return Vector3.Angle(target.transform.position - artillery.position, artillery.forward) < _attackAngle;
            }

            private void TryAttack(Component target)
            {
                if (!Initializer.Timer.IsTimeOver) return;
                if (!target.TryGetComponent<Damageble>(out var damageable) || damageable.Team == 1) return;

                Object.Instantiate
                (
                    Initializer.ProjectilePrefab,
                    Initializer.ArtilleryTransform.position,
                    Initializer.ArtilleryTransform.rotation
                ).LaunchTo(damageable, Explosion);

                Initializer.Timer.Time = Initializer.CurrentStats.AttackCooldown;
            }

            private void Explosion(Damageble target)
            {
                System.Array.Clear(_exploded, 0, _explodedSize);
                _explodedSize = Physics.OverlapSphereNonAlloc(target.transform.position,
                    Initializer.CurrentStats.ExplosionRadius, _exploded);

                for (var i = 0; i < _explodedSize; i++)
                    if (_exploded[i].TryGetComponent<Damageble>(out var damageable) && damageable.Team != 1)
                        damageable.Damage(Initializer.CurrentStats.Damage * Initializer.DamageEnergyMultiplier);
            }
        }
    }
}