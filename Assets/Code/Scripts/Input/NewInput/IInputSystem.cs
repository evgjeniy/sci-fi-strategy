namespace SustainTheStrain.Input
{
    public interface IInputSystem
    {
        public IInputData InputData { get; }
        public void Idle();
        public void Select(IInputSelectable selectable);
        public void Enable();
        public void Disable();
    }
}