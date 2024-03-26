using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class ArtilleryStateMachine : StateMachine<ArtilleryStateMachine>
    {
        private Artillery Context { get; }
        private Area Area { get; }
        private Timer Timer { get; } = new();

        private Transform ArtilleryTransform => Context.transform;
        private ArtilleryData.Stats CurrentStats => Context.CurrentStats;
        private Projectile ProjectilePrefab => Context.Data.Projectile;
        private float DamageEnergyMultiplier => Context.BuildingSystem.DamageMultiplier;
        private float CooldownEnergyMultiplier => Context.BuildingSystem.CooldownMultiplier;

        public ArtilleryStateMachine(Artillery context)
        {
            Context = context;
            
            Area = new Area(GetPosition, GetAttackRadius, GetAttackMask);
            TransitionsEnabled = false;

            AddStates(new IdleState(this), new AttackState(this));
            SetState<IdleState>();
        }

        public new void Run()
        {
            base.Run();

            Context.ZoneVisualizer.Angle = 360f;
            Context.ZoneVisualizer.Radius = GetAttackRadius();
        }

        private Vector3 GetPosition() => Context.transform.position;
        private float GetAttackRadius() => Context.CurrentStats.AttackRadius;
        private int GetAttackMask() => Context.Data.AttackMask.value;
    }
}