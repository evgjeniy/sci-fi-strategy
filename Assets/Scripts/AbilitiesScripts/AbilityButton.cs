using UnityEngine.UI;

namespace SustainTheStrain.AbilitiesScripts
{
    public class AbilityButton
    {
        private readonly Button _button;
        private readonly Slider _slider;
        private bool ready;
        

        public AbilityButton(Button b, Slider s)
        {
            _button = b;
            _slider = s;
            ready = true;
        }

        public bool ChangeReady() => ready ^= true;

        public Button GetButton() => _button;

        public Slider GetSlider() => _slider;

        public bool IsReady() => ready;
    }
}
