namespace SustainTheStrain.Input
{
    public interface IInputSelectable
    {
        public void OnSelected() {}
        public void OnDeselected() {}
        public IInputState OnSelectedUpdate(IInputState currentState, UnityEngine.Ray ray) => currentState;
        public IInputState OnSelectedLeftClick(IInputState currentState, UnityEngine.Ray ray) => currentState;
        public IInputState OnSelectedRightClick(IInputState currentState, UnityEngine.Ray ray) => new InputIdleState();
    }
}