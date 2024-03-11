namespace SustainTheStrain.Units.StateMachine
{
    public abstract class State<T> : IState
    {
        protected T context;
        protected StateMachine stateMachine;

        public State(T context, StateMachine stateMachine)
        {
            this.context = context;
            this.stateMachine = stateMachine;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void FrameUpdate();
        public abstract void PhysicsUpdate();
    }
}
