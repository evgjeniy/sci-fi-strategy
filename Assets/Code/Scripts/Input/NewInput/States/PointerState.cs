namespace SustainTheStrain.Input
{
    public class PointerState : IInputState
    {
        private readonly IInputSelectable _selectable;

        public PointerState(IInputSelectable selectable) => _selectable = selectable;

        public void Enter() => _selectable.OnPointerEnter();

        public void Exit() => _selectable.OnPointerExit();

        public IInputState ProcessMouseMove(IInputSystem context)
        {
            if (!context.MousePosition.TryGetCameraRay(out var screenRay)) return this;

            if (screenRay.TryGetRaycastComponent<IInputSelectable>(context.Settings, out var selectable))
                return _selectable == selectable ? this : new PointerState(selectable);

            return new IdleState();
        }

        public IInputState ProcessLeftClick(IInputSystem _) => new SelectableState(_selectable);

        public IInputState ProcessRightClick(IInputSystem _) => this;
    }
}