namespace SustainTheStrain.Input
{
    public class InputIdleState : IInputState
    {
        public IInputState ProcessFrame(IInputData inputData)
        {
            return inputData.IsPointerUnder<IInputPointerable>(out var pointerable)
                ? new InputPointerState(pointerable)
                : this;
        }

        public IInputState ProcessLeftClick(IInputData inputData)
        {
            return inputData.IsPointerUnder<IInputSelectable>(out var selectable)
                ? new InputSelectState(selectable)
                : this;
        }

        public IInputState ProcessRightClick(IInputData inputData) => this;
    }
}