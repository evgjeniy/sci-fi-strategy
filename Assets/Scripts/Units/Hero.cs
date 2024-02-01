using System;
using SustainTheStrain.Input;
using SustainTheStrain.Input.States;
using SustainTheStrain.Units.StateMachine.ConcreteStates;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units
{
    public class Hero : Unit
    {
        [Inject] private IHeroInput _heroInput;

        #region State Machine Variables

        protected HeroIdleState _idleState;
        protected HeroMoveState _moveState;
        protected UnitAttackState _attackState;
        protected UnitAgroState _aggroState;

        #endregion
        
        private Vector3 _destination;

        public Vector3 Destination => _destination;
        public bool IsMoving { get; set; }
        
        private void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();

            _idleState = new HeroIdleState(this, _stateMachine);
            _attackState = new UnitAttackState(this, _stateMachine);
            _moveState = new HeroMoveState(this, _stateMachine);
            _aggroState = new UnitAgroState(this, _stateMachine);
            _aggroState.Init(_attackState, _idleState);
            _attackState.Init(_aggroState, _idleState);
            _moveState.Init(_idleState);
            _idleState.Init(_aggroState);

            _stateMachine.Initialize(_idleState);
        }

        public void Move(Vector3 destination)
        {
            _destination = destination;
            
            _stateMachine.ChangeState(_moveState);
        }
    }
}