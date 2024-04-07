using UnityEngine;

namespace SustainTheStrain.Input
{
    public class SelectableState : IInputState
    {
        private readonly IInputSelectable _selectable;

        private IInputSelectable _pointerSelectable;

        public SelectableState(IInputSelectable selectable) => _selectable = selectable;

        public void Enter() => _selectable.OnSelected();

        public void Exit() => _selectable.OnDeselected();

        public IInputState ProcessMouseMove(IInputSystem context)
        {
            if (context.MousePosition.TryGetCameraRay(out var screenRay) is false)
                return this;

            UpdatePointerSelectable(context.Settings, screenRay);

            return _selectable.OnSelectedUpdate(this, screenRay);
        }

        public IInputState ProcessLeftClick(IInputSystem context)
        {
            if (context.MousePosition.TryGetCameraRay(out var screenRay) is false)
                return this;

            if (_pointerSelectable != null)
            {
                _pointerSelectable.OnPointerExit();
                return new SelectableState(_pointerSelectable);
            }

            return _selectable.OnSelectedLeftClick(this, screenRay);
        }

        public IInputState ProcessRightClick(IInputSystem context)
        {
            var hasRay = context.MousePosition.TryGetCameraRay(out var screenRay);
            return hasRay ? _selectable.OnSelectedRightClick(this, screenRay) : this;
        }

        private void UpdatePointerSelectable(InputSettings settings, Ray screenRay)
        {
            var hasSelectable = screenRay.TryGetRaycastComponent<IInputSelectable>(settings, out var pointerSelectable);
            if (pointerSelectable == _selectable) return;
            
            if (hasSelectable)
            {
                if (_pointerSelectable != null) return;
                
                _pointerSelectable = pointerSelectable;
                _pointerSelectable.OnPointerEnter();
            }
            else
            {
                if (_pointerSelectable == null) return;
                
                _pointerSelectable.OnPointerExit();
                _pointerSelectable = null;
            }
        }
    }
}