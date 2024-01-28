using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Buildings.Data;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM.RocketStates
{
    public partial class RocketStateMachine : StateMachine<RocketStateMachine>
    {
        private readonly Rocket _rocket;

        private Area Area { get; }
        private Timer Timer { get; } = new();

        private Transform RocketTransform => _rocket.transform;
        private RocketData.Stats CurrentStats => _rocket.CurrentStats;
        private Projectile ProjectilePrefab => _rocket.Data.Projectile;

        public RocketStateMachine(Rocket rocket)
        {
            _rocket = rocket;
            Area = new Area(GetRocketPosition, GetAttackRadius, GetAttackMask);
            TransitionsEnabled = false;
            
            AddStates(new IdleState(this), new RotateState(this), new AttackState(this));
            
            SetState<IdleState>();
        }

        private Vector3 GetRocketPosition() => _rocket.transform.position;
        private float GetAttackRadius() => _rocket.CurrentStats.AttackRadius;
        private int GetAttackMask() => _rocket.Data.AttackMask.value;
    }
}