namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class RecruitMoveState : State<Recruit>
    {
        private IState _idleState;
        
        public RecruitMoveState(Recruit context, StateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public void Init(IState idleState)
        {
            _idleState = idleState;
        }

        public override void EnterState()
        {
            context.SwitchPathFollower(context.NavPathFollower);
            
            
            if(!context.NavPathFollower.MoveToWithCheck(context.GuardPosition))
                context.StateMachine.ChangeState(_idleState);
            
            context.Duelable.BreakDuel();
            context.IsMoving = true;
        }

        public override void ExitState()
        {
            context.CurrentPathFollower.Stop();
            context.IsMoving = false;
        }

        public override void FrameUpdate()
        {
            if (context.NavPathFollower.IsDestinationReached())
            {
                context.StateMachine.ChangeState(_idleState);
            }
        }

        public override void PhysicsUpdate()
        {
        }
    }
}
