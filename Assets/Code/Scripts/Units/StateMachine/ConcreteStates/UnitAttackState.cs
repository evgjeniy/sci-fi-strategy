using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class UnitAttackState : State<Unit>
    {
        private IState _aggroState;
        private IState _idleState;

        private float _attackTime;
        private static readonly int AttackMode = Animator.StringToHash("AttackMode");

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
            if (context.Animator != null)
            {
                context.Animator.SetBool(AttackMode, true);
                context.Animator.SetBool("Moving", false);
            }

            _attackTime = 0;
            
            context.SwitchPathFollower(context.NavPathFollower);
            context.CurrentPathFollower.Stop();
        }

        public override void ExitState()
        {
            if(context.Animator != null)
                context.Animator.SetBool(AttackMode, false);
            
            //context.NavPathFollower.MoveTo(context.transform.position);
        }

        public override void FrameUpdate()
        {
            _attackTime += Time.deltaTime;

            if (!context.Duelable.HasOpponent)
            {
                context.StateMachine.ChangeState(_idleState);
                return;
            }

            if (!context.CheckIfInAttackZone(context.Duelable.Opponent))
            {
                context.StateMachine.ChangeState(_aggroState);
                return;
            }

            if (context.IsFreezed) return;
            
            if(_attackTime > context.DamagePeriod)
            {
                _attackTime = 0;
                context.Duelable.Opponent.Damageable.Damage(context.Damage);
            }
            LookAtOpponent();
        }

        public override void PhysicsUpdate()
        {
            context.FindOpponent().IfNotNull(duelable =>
            {
                if(context.Duelable.Opponent != duelable) context.Duelable.RequestDuel(duelable);
            });
        }

        private void LookAtOpponent()
        {
            Quaternion lookRotation = 
                Quaternion.LookRotation((context.Duelable.Opponent.transform.position - context.transform.position).normalized);
            
            context.transform.rotation = 
                Quaternion.Slerp(context.transform.rotation, lookRotation, Time.deltaTime * 3);
        }
    }
}
