namespace SustainTheStrain.Input
{
    public class InputPointerState : IInputState
    {
        private readonly IInputPointerable _pointerable;

        public InputPointerState(IInputPointerable pointerable) => _pointerable = pointerable;

        public void Enter() => _pointerable.OnPointerEnter();

        public void Exit() => _pointerable.OnPointerExit();

        public IInputState ProcessFrame(IInputData inputData)
        {
            if (inputData.IsPointerUnder<IInputPointerable>(out var pointerable))
                return _pointerable == pointerable ? this : new InputPointerState(pointerable);

            return new InputIdleState();
        }

        public IInputState ProcessLeftClick(IInputData inputData)
        {
            if (inputData.IsPointerUnder<IInputSelectable>(out var selectable))
                return new InputSelectState(selectable);

            return this;
        }

        public IInputState ProcessRightClick(IInputData _) => this;
    }
}