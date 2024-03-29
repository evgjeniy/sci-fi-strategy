namespace SustainTheStrain.Input
{
    public interface IInputState
    {
        public void Enter() {}
        public void Exit() {}

        public IInputState ProcessMouseMove(IInputSystem context);
        public IInputState ProcessLeftClick(IInputSystem context);
        public IInputState ProcessRightClick(IInputSystem context);
    }
}