namespace SustainTheStrain.Units.StateMachine
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            CurrentState.EnterState();
        }

        public void ChangeState(IState newState)
        {
            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}
