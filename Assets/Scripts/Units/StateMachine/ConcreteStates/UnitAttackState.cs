using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class UnitAttackState : State<Unit>
    {
        private IState _aggroState;
        private IState _idleState;

        private float _attackTime;

        public UnitAttackState(Unit context, StateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public void Init(IState aggroState, IState idleState)
        {
            _aggroState = aggroState;
            _idleState = idleState;
        }


        public override void EnterState()
        {
            Debug.Log(string.Format("[StateMachine {0}] UnitAttackState entered", context.gameObject.name));

            _attackTime = 0;
            
            context.SwitchPathFollower(context.NavPathFollower);
            context.CurrentPathFollower.Stop();
        }

        public override void ExitState()
        {
            
        }

        public override void FrameUpdate()
        {
            if (context.Duelable.Opponent == null) context.StateMachine.ChangeState(_idleState);

            if (!context.IsOpponentInAttackZone) context.StateMachine.ChangeState(_aggroState);

            if(_attackTime > context.DamagePeriod)
            {
                _attackTime = 0;
                context.Duelable.Opponent.Damageble.Damage(context.Damage);
            }

            _attackTime += Time.deltaTime;
        }

        public override void PhysicsUpdate()
        {
        
        }
    }
}
