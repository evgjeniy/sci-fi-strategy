using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class HeroIdleState : State<Hero>
    {
        private IState _aggroState;
        
        public HeroIdleState(Hero context, StateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public void Init(IState aggroState)
        {
            _aggroState = aggroState;
        }
        
        public override void EnterState()
        {
            Debug.Log(string.Format("[StateMachine {0}] HeroIdleState entered", context.gameObject.name));

            context.SwitchPathFollower(context.NavPathFollower);
            context.CurrentPathFollower.Stop();
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
            context.FindOpponent().IfNotNull(duelable => context.Duelable.RequestDuel(duelable));
        }
    }
}
