using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Buildings.Data;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.LaserStates
{
    public partial class LaserStateMachine : StateMachine<LaserStateMachine>
    {
        private readonly Laser _laser;

        private Area Area { get; }
        private Timer Timer { get; } = new();

        private Transform LaserTransform => _laser.transform;
        private LaserData.Stats CurrentStats => _laser.CurrentStats;
        private float DamageEnergyMultiplier => _laser.BuildingSystem.DamageMultiplier;
        private float CooldownEnergyMultiplier => _laser.BuildingSystem.CooldownMultiplier;

        public LaserStateMachine(Laser laser)
        {
            _laser = laser;
            Area = new Area(GetPosition, GetAttackRadius, GetAttackMask);
            TransitionsEnabled = false;

            AddStates(new IdleState(this), new AttackState(this));
            SetState<IdleState>();
        }

        private Vector3 GetPosition() => _laser.transform.position;
        private float GetAttackRadius() => _laser.CurrentStats.AttackRadius;
        private int GetAttackMask() => _laser.Data.AttackMask.value;
    }
}