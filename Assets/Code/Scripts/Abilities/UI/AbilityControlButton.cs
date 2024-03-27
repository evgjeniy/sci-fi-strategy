using SustainTheStrain.Input.UI;
using UnityEngine.UI;

namespace SustainTheStrain.Abilities
{
    public class AbilityControlButton// : EnergySystemControllButton
    {
        private readonly InputSystemButtonBridge _button;
        private readonly Slider _slider;

        public AbilityControlButton(InputSystemButtonBridge b, Slider s)
        {
            _button = b;
            _slider = s;
        }
        
        public InputSystemButtonBridge GetButton() => _button;

        public Slider GetSlider() => _slider;
        
    }
}
