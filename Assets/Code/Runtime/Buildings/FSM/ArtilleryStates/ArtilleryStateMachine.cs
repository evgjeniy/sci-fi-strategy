﻿using NTC.FiniteStateMachine;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class ArtilleryStateMachine : StateMachine<ArtilleryStateMachine>
    {
        private readonly Artillery _artillery;
        private readonly ParticleSystem _buildingRadius;

        private Area<Collider> Area { get; }
        private Timer Timer { get; } = new();

        private Transform ArtilleryTransform => _artillery.transform;
        private ArtilleryData.Stats CurrentStats => _artillery.CurrentStats;
        private Projectile ProjectilePrefab => _artillery.Data.Projectile;
        private float DamageEnergyMultiplier => _artillery.BuildingSystem.DamageMultiplier;
        private float CooldownEnergyMultiplier => _artillery.BuildingSystem.CooldownMultiplier;

        public ArtilleryStateMachine(Artillery artillery)
        {
            _artillery = artillery;
            _buildingRadius = Object.Instantiate(Resources.Load<ParticleSystem>("BuildingData/Radius"), artillery.transform);
            _buildingRadius.transform.localPosition = Vector3.zero;

            Area = new Area<Collider>(GetPosition, GetAttackRadius, GetAttackMask);
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

        private Vector3 GetPosition() => _artillery.transform.position;
        private float GetAttackRadius() => _artillery.CurrentStats.AttackRadius;
        private int GetAttackMask() => _artillery.Data.AttackMask.value;
    }
}