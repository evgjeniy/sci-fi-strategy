using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class ArtilleryStateMachine : StateMachine<ArtilleryStateMachine>
    {
        private readonly Artillery _artillery;

        private Area Area { get; }
        private Timer Timer { get; } = new();

        private Transform ArtilleryTransform => _artillery.transform;
        private ArtilleryData.Stats CurrentStats => _artillery.CurrentStats;
        private Projectile ProjectilePrefab => _artillery.Data.Projectile;
        private float DamageEnergyMultiplier => _artillery.BuildingSystem.DamageMultiplier;
        private float CooldownEnergyMultiplier => _artillery.BuildingSystem.CooldownMultiplier;

        public ArtilleryStateMachine(Artillery artillery)
        {
            _artillery = artillery;
            
            Area = new Area(GetPosition, GetAttackRadius, GetAttackMask);
            TransitionsEnabled = false;

            AddStates(new IdleState(this), new AttackState(this));
            SetState<IdleState>();
        }

        public new void Run()
        {
            base.Run();

            _artillery.ZoneVisualizer.Angle = 360f;
            _artillery.ZoneVisualizer.Radius = GetAttackRadius();
        }

        private Vector3 GetPosition() => _artillery.transform.position;
        private float GetAttackRadius() => _artillery.CurrentStats.AttackRadius;
        private int GetAttackMask() => _artillery.Data.AttackMask.value;
    }
}