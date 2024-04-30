using UnityEngine;
using UnityEngine.Extensions;
using static UnityEngine.UI.CanvasScaler;

namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class RecruitIdleState : State<Recruit>
    {
        private IState _aggroState;

        public RecruitIdleState(Recruit context, StateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public void Init(IState aggroState)
        {
            _aggroState = aggroState;
        }

        public override void EnterState()
        {
            Debug.Log(string.Format("[StateMachine {0}] RecruitIdleState entered", context.gameObject.name));

            context.SwitchPathFollower(context.NavPathFollower);
            context.CurrentPathFollower.Stop();
            
            context.Animator.SetBool("Moving", false);
        }

        public override void ExitState()
        {
            context.CurrentPathFollower.Stop();
        }

        public override void FrameUpdate()
        {
            if (context.Duelable.HasOpponent) context.StateMachine.ChangeState(_aggroState);
        }

        public override void PhysicsUpdate()
        {
            context.FindOpponent().IfNotNull(duelable => { context.Duelable.RequestDuel(duelable); Debug.Log("Recruit Opponent"); });
        }
    }
}
