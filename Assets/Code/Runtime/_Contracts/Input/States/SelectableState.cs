namespace SustainTheStrain._Contracts
{
    public class SelectableState : IInputState
    {
        private readonly IInputSelectable _selectable;

        public SelectableState(IInputSelectable selectable) => _selectable = selectable;

        public void Enter() => _selectable.OnSelected();

        public void Exit() => _selectable.OnDeselected();

        public IInputState ProcessMouseMove(IInputSystem context)
        {
            if (context.MousePosition.TryGetCameraRay(out var screenRay))
                _selectable.OnUpdateSelected(screenRay);

            return this;
        }

        public IInputState ProcessLeftClick(IInputSystem context)
        {
            if (context.MousePosition.TryGetCameraRay(out var screenRay)) return this;

            if (screenRay.TryGetRaycastComponent<IInputSelectable>(context.Settings, out var selectable))
                return _selectable == selectable ? this : new SelectableState(selectable);

            _selectable.OnClickSelected(screenRay);
            return this;
        }

        public IInputState ProcessRightClick(IInputSystem _) => new IdleState();
    }
}