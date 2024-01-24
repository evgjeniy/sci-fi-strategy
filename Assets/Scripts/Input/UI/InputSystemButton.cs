using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SustainTheStrain.Input.UI
{
    [RequireComponent(typeof(InputSystemButtonBridge))]
    public class InputSystemButton : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [field: SerializeField] public Button.ButtonClickedEvent OnClick { get; set; } = new();

        private InputSystemButtonBridge _bridge;

        protected override void Awake()
        {
            _bridge = GetComponent<InputSystemButtonBridge>();
            base.Awake();
        }

        private async void Press()
        {
            if (!IsActive() || !IsInteractable()) return;

            UISystemProfilerApi.AddMarker($"{nameof(InputSystemButton)}.{nameof(OnClick)}", this);
            await _bridge.Click();
            OnClick.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            Press();
        }

        public virtual async void OnSubmit(BaseEventData eventData)
        {
            Press();

            if (!IsActive() || !IsInteractable()) return;

            DoStateTransition(SelectionState.Pressed, false);
            await OnFinishSubmit();
        }

        private async UniTask OnFinishSubmit()
        {
            for (var time = 0.0f; time < colors.fadeDuration; time += Time.unscaledDeltaTime)
                await UniTask.NextFrame();

            DoStateTransition(currentSelectionState, false);
        }
    }
}