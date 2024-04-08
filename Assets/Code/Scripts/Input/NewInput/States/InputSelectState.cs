namespace SustainTheStrain.Input
{
    public class InputSelectState : IInputState
    {
        private readonly IInputSelectable _selectable;

        private IInputPointerable _pointerable;

        public InputSelectState(IInputSelectable selectable) => _selectable = selectable;

        public void Enter() => _selectable.OnSelected();

        public void Exit()
        {
            _pointerable?.OnPointerExit();
            _selectable.OnDeselected();
        }

        public IInputState ProcessFrame(IInputData inputData)
        {
            if (inputData.MousePosition.TryGetCameraRay(out var screenRay) is false)
                return this;

            UpdatePointerable(inputData, screenRay);

            return _selectable.OnSelectedUpdate(this, screenRay);
        }

        public IInputState ProcessLeftClick(IInputData inputData)
        {
            if (inputData.MousePosition.TryGetCameraRay(out var screenRay) is false)
                return this;

            if (_pointerable is IInputSelectable selectable)
            {
                _pointerable.OnPointerExit();
                return new InputSelectState(selectable);
            }

            return _selectable.OnSelectedLeftClick(this, screenRay);
        }

        public IInputState ProcessRightClick(IInputData inputData)
        {
            var hasRay = inputData.MousePosition.TryGetCameraRay(out var screenRay);
            return hasRay ? _selectable.OnSelectedRightClick(this, screenRay) : this;
        }

        private void UpdatePointerable(IInputData inputData, UnityEngine.Ray screenRay)
        {
            var hasPointerable = screenRay.TryGetComponent<IInputPointerable>(inputData, out var pointerable);

            if (pointerable == _selectable)
                return;

            if (hasPointerable)
            {
                if (_pointerable != null) return;

                _pointerable = pointerable;
                _pointerable.OnPointerEnter();
            }
            else
            {
                if (_pointerable == null) return;

                _pointerable.OnPointerExit();
                _pointerable = null;
            }
        }
    }
}