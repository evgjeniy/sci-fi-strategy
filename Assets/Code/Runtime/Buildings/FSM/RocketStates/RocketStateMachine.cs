using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class RocketStateMachine : StateMachine<RocketStateMachine>
    {
        private readonly ParticleSystem _buildingRadius;

        private Rocket Context { get; }
        private Area Area { get; }
        private Timer Timer { get; } = new();

        private Transform RocketTransform => Context.transform;
        private RocketData.Stats CurrentStats => Context.CurrentStats;
        private Projectile ProjectilePrefab => Context.Data.Projectile;
        private float DamageEnergyMultiplier => Context.BuildingSystem.DamageMultiplier;
        private float CooldownEnergyMultiplier => Context.BuildingSystem.CooldownMultiplier;

        public RocketStateMachine(Rocket rocket)
        {
            Context = rocket;
            _buildingRadius = Object.Instantiate(Resources.Load<ParticleSystem>("BuildingData/Radius"), rocket.transform);
            _buildingRadius.transform.localPosition = Vector3.zero;

            Area = new Area(GetPosition, GetAttackRadius, GetAttackMask);
            TransitionsEnabled = false;

            AddStates(new IdleState(this), new AttackState(this));
            SetState<IdleState>();
        }

        public new void Run()
        {
            base.Run();
            
            var shape = _buildingRadius.shape;
            shape.radius = GetAttackRadius();
        }

        private Vector3 GetPosition() => Context.transform.position;
        private float GetAttackRadius() => Context.CurrentStats.AttackRadius;
        private int GetAttackMask() => Context.Data.AttackMask.value;
    }
}