using UnityEngine;
using Zenject;

namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class UnitAgroState : State<Unit>
    {
        private IState _attackState;
        private IState _idleState;

        private float _disaggroTime = 5f;
        private float _time = 0f;

        public UnitAgroState(Unit context, StateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public void Init(IState attackState, IState idleState)
        {
            _attackState = attackState;
            _idleState = idleState;
        }

        public override void EnterState()
        {
            context.SwitchPathFollower(context.NavPathFollower);
            context.NavPathFollower.MoveTo(context.Duelable.Opponent.DuelPosition);

            _time = 0;
            
            Debug.Log(string.Format("[StateMachine {0}] UnitAgroState entered", context.gameObject.name));
            Debug.Log(string.Format("[StateMachine {0}] OPPONENT {1}", context.gameObject.name, context.Duelable.Opponent.gameObject.name));
        }

        public override void ExitState()
        {
            context.CurrentPathFollower.Stop();
        }

        public override void FrameUpdate()
        {
            if(!context.IsOpponentInAggroZone && _time > _disaggroTime)
            {
                context.Duelable.BreakDuel();
                return;
            }

            if (!context.Duelable.HasOpponent)
            {
                context.StateMachine.ChangeState(_idleState);
                return;
            }

            context.NavPathFollower.MoveTo(context.Duelable.Opponent.DuelPosition);

            if(context.IsOpponentInAttackZone)
                context.StateMachine.ChangeState(_attackState);

            _time += Time.deltaTime;
        }

        public override void PhysicsUpdate()
        {
        }
    }
}
