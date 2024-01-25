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
            if(context.transform.position != context.GuardPosition)
                context.NavPathFollower.MoveTo(context.GuardPosition);
        }

        public override void ExitState()
        {
            context.NavPathFollower.Stop();
        }

        public override void FrameUpdate()
        {
       
        }

        public override void PhysicsUpdate()
        {
        
        }
    }
}
