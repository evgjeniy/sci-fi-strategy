namespace SustainTheStrain.Input
{
    public class IdleState : IInputState
    {
        public virtual IInputState ProcessMouseMove(IInputSystem context)
        {
            if (context.MousePosition.TryGetCameraRay(out var screenRay) is false) return this;

            if (screenRay.TryGetRaycastComponent<IInputSelectable>(context.Settings, out var selectable))
                return new PointerState(selectable);

            return this;
        }

        public virtual IInputState ProcessRightClick(IInputSystem _) => this;
        public virtual IInputState ProcessLeftClick(IInputSystem _) => this;
    }
}