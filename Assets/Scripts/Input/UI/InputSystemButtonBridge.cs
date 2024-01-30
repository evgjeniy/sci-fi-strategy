using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace SustainTheStrain.Input.UI
{
    public class InputSystemButtonBridge : OnScreenControl
    {
        [SerializeField, InputControl(layout = "Button")] private string _controlPath = "<Keyboard>/r";
        
        public void SetButton(string key) => controlPathInternal = $"<Keyboard>/{key}";
        public void SetNumberButton(int number) => controlPathInternal = $"<Keyboard>/{number}";
        
        protected override string controlPathInternal
        {
            get => _controlPath;
            set
            {
                OnDisable();
                _controlPath = value;
                OnEnable();
            }
        }

        public async UniTask Click()
        {
            SendValueToControl(1.0f);
            await UniTask.NextFrame();
            SendValueToControl(0.0f);
        }
    }
}