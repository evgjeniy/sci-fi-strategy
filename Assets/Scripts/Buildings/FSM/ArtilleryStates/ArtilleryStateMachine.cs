using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Buildings.Data;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.ArtilleryStates
{
    public partial class ArtilleryStateMachine : StateMachine<ArtilleryStateMachine>
    {
        private readonly Artillery _artillery;

        private Area Area { get; }
        private Timer Timer { get; } = new();

        private Transform ArtilleryTransform => _artillery.transform;
        private ArtilleryData.Stats CurrentStats => _artillery.CurrentStats;
        private Projectile ProjectilePrefab => _artillery.Data.Projectile;

        public ArtilleryStateMachine(Artillery artillery)
        {
            _artillery = artillery;
            Area = new Area(GetPosition, GetAttackRadius, GetAttackMask);
            TransitionsEnabled = false;

            AddStates(new IdleState(this), new AttackState(this));
            SetState<IdleState>();
        }

        private Vector3 GetPosition() => _artillery.transform.position;
        private float GetAttackRadius() => _artillery.CurrentStats.AttackRadius;
        private int GetAttackMask() => _artillery.Data.AttackMask.value;
    }
}