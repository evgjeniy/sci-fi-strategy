using SustainTheStrain.Input.UI;
using UnityEngine.UI;

namespace SustainTheStrain.AbilitiesScripts
{
    public class AbilityButton
    {
        private readonly InputSystemButtonBridge _button;
        private readonly Slider _slider;
        private bool ready;
        

        public AbilityButton(InputSystemButtonBridge b, Slider s)
        {
            _button = b;
            _slider = s;
            ready = true;
        }

        public bool ChangeReady() => ready ^= true;

        public InputSystemButtonBridge GetButton() => _button;

        public Slider GetSlider() => _slider;

        public bool IsReady() => ready;
    }
}
