using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace SustainTheStrain.Input.UI
{
    public class InputSystemButtonBridge : OnScreenControl
    {
        [SerializeField, InputControl(layout = "Button")] private string _controlPath = "<Keyboard>/r";
        
        [field: SerializeField] public float Value { get; set; } = 1.0f;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        public async UniTask Click()
        {
            SendValueToControl(Value);
            await UniTask.NextFrame();
            SendValueToControl(0.0f);
        }
    }
}