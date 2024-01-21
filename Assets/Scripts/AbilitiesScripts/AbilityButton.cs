using UnityEngine.UI;

public class AbilityButton
{
    private Button button;
    private Slider slider;
    public Button getButton() => button;
    public Slider getSlider() => slider;
    public AbilityButton(Button b, Slider s)
    {
        button = b;
        slider = s;
    }
}
