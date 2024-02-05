namespace SustainTheStrain.Units.StateMachine.ConcreteStates
{
    public class KamikazeAttackState : State<Unit>
    {
        private IState _aggroState;
        private IState _idleState;
        

        public KamikazeAttackState(Unit context, StateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public void Init(IState aggroState, IState idleState)
        {
            _aggroState = aggroState;
            _idleState = idleState;
        }


        public override void EnterState()
        {
            context.SwitchPathFollower(context.NavPathFollower);
            context.CurrentPathFollower.Stop();
        }

        public override void ExitState()
        {
            
        }

        public override void FrameUpdate()
        {
            if (!context.Duelable.HasOpponent)
            {
                context.StateMachine.ChangeState(_idleState);
                return;
            }

            if (!context.IsOpponentInAttackZone)
            {
                context.StateMachine.ChangeState(_aggroState);
                return;
            }

            context.Duelable.Opponent.Damageble.Damage(context.Damage);
            context.Duelable.Damageble.Damage(1000);
        }

        public override void PhysicsUpdate()
        {
        
        }
    }
}
