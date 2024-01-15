public abstract class State<T>
{
    protected T context;
    protected StateMachine<T> stateMachine;

    public State(T context, StateMachine<T> stateMachine)
    {
        this.context = context;
        this.stateMachine = stateMachine;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void FrameUpdate();
    public abstract void PhysicsUpdate();
}
