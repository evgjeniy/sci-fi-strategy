using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using SustainTheStrain.Units;
using SustainTheStrain.Units.PathFollowers;
using SustainTheStrain.Units.StateMachine.ConcreteStates;
using UnityEngine;

namespace SustainTheStrain
{
    public class Kamikaze : Enemy
    {
        private void Start()
        {
            Init();
            InitLogic();
        }

        protected override void InitLogic()
        {
            _splineMoveState = new EnemySplineMoveState(this, _stateMachine);
            var attackState = new KamikazeAttackState(this, _stateMachine);
            _aggroState = new UnitAgroState(this, _stateMachine);
            _splineMoveState.Init(_aggroState);
            _aggroState.Init(attackState, _splineMoveState);
            attackState.Init(_aggroState, _splineMoveState);

            _stateMachine.Initialize(_splineMoveState);
        }
    }
}
