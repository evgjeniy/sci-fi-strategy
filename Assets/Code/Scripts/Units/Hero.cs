using SustainTheStrain.Abilities;
using SustainTheStrain.Input;
using SustainTheStrain.Units.StateMachine.ConcreteStates;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units
{
    public class Hero : Unit, IInputSelectable, IInputPointerable
    {
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

        private void Update()
        {
            Animator.SetBool("Moving", !NavPathFollower.IsDestinationReached() || !NavPathFollower.IsStopped);
            StateMachine.CurrentState.FrameUpdate();
        }

        public IInputState OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (Physics.Raycast(ray, out var hit) is false)
                return currentState;
            
            Move(hit.point);
            return new InputIdleState();
        }

        public void OnPointerEnter() => GetComponent<Outline>().IfNotNull(x => x.Enable());
        public void OnPointerExit() => GetComponent<Outline>().IfNotNull(x => x.Disable());

        public void Move(Vector3 destination)
        {
            _destination = destination;
            _stateMachine.ChangeState(_moveState);
        }
    }
}