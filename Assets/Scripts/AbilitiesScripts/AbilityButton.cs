using UnityEngine.UI;

namespace SustainTheStrain.AbilitiesScripts
{
    public class AbilityButton
    {
        private readonly Button _button;
        private readonly Slider _slider;
        
        public AbilityButton(Button b, Slider s)
        {
            _button = b;
            _slider = s;
        }
        
        public Button GetButton() => _button;
        public Slider GetSlider() => _slider;
    }
}
