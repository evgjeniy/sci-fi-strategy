namespace SustainTheStrain.Input
{
    public class SelectableState : IInputState
    {
        private readonly IInputSelectable _selectable;

        public SelectableState(IInputSelectable selectable) => _selectable = selectable;

        public void Enter() => _selectable.OnSelected();

        public void Exit() => _selectable.OnDeselected();

        public IInputState ProcessMouseMove(IInputSystem context) =>
            context.MousePosition.TryGetCameraRay(out var screenRay)
                ? _selectable.OnSelectedUpdate(this, screenRay)
                : this;

        public IInputState ProcessLeftClick(IInputSystem context)
        {
            if (context.MousePosition.TryGetCameraRay(out var screenRay) is false)
                return this;

            if (screenRay.TryGetRaycastComponent<IInputSelectable>(context.Settings, out var selectable))
                return _selectable == selectable ? this : new SelectableState(selectable);

            return _selectable.OnSelectedLeftClick(this, screenRay);
        }

        public IInputState ProcessRightClick(IInputSystem context) =>
            context.MousePosition.TryGetCameraRay(out var screenRay)
                ? _selectable.OnSelectedRightClick(this, screenRay)
                : this;
    }
}