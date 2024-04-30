using SustainTheStrain.Scriptable.Buildings;
using SustainTheStrain.Units.StateMachine.ConcreteStates;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units
{
    public class Recruit : Unit
    {
        #region State Machine Variables

        protected RecruitIdleState _recruitIdleState;
        protected RecruitMoveState _recruitMoveState;
        protected UnitAttackState _attackState;
        protected UnitAgroState _aggroState;

        #endregion

        private Vector3 _guardPosition;
        public bool IsMoving { get; set; }
        public Vector3 GuardPosition => _guardPosition;

        private void Start()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();

            _guardPosition = transform.position;

            _recruitIdleState = new RecruitIdleState(this, _stateMachine);
            _attackState = new UnitAttackState(this, _stateMachine);
            _aggroState = new UnitAgroState(this, _stateMachine);
            _recruitMoveState = new RecruitMoveState(this, _stateMachine);
            _recruitIdleState.Init(_aggroState);
            _aggroState.Init(_attackState, _recruitIdleState);
            _attackState.Init(_aggroState, _recruitIdleState);
            _recruitMoveState.Init(_aggroState);

            _stateMachine.Initialize(_recruitIdleState);
        }

        public void UpdatePosition(Vector3 position)
        {
            _guardPosition = position;
            _stateMachine.ChangeState(_recruitMoveState);
        }

        public void UpdateStats(BarrackData.Stats stats)
        {
            Damage = stats.UnitAttackDamage;
            DamagePeriod = stats.UnitAttackCooldown;
            
            if (TryGetComponent<IHealth>(out var health))
                health.MaxHP = health.CurrentHP = stats.UnitMaxHealth;
        }
        
        public class Factory : IFactory<Recruit>
        {
            private readonly Recruit _refRecruit;

            public Factory(Recruit refRecruit) => _refRecruit = refRecruit;

            public Recruit Create() => Instantiate(_refRecruit);
        }
    }
}
