namespace SustainTheStrain.Input
{
    public interface IInputState
    {
        public void Enter() {}
        public void Exit() {}

        public IInputState ProcessFrame(IInputData inputData);
        public IInputState ProcessLeftClick(IInputData inputData);
        public IInputState ProcessRightClick(IInputData inputData);
    }
}