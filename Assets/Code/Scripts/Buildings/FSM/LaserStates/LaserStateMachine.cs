using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class LaserStateMachine : StateMachine<LaserStateMachine>
    {
        private Laser Context { get; }
        private Area Area { get; }
        private Timer Timer { get; } = new();

        private Transform LaserTransform => Context.transform;
        private LaserData.Stats CurrentStats => Context.CurrentStats;
        private float DamageEnergyMultiplier => Context.BuildingSystem.DamageMultiplier;
        private float CooldownEnergyMultiplier => Context.BuildingSystem.CooldownMultiplier;

        public LaserStateMachine(Laser laser)
        {
            Context = laser;

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