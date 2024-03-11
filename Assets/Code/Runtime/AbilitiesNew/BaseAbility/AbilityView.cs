using SustainTheStrain._Architecture;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.AbilitiesNew
{
    public class AbilityView : MonoBehaviour, IView<AbilityData>
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public Slider Slider { get; private set; }

        public virtual void Display(AbilityData model) => Slider.value = Mathf.InverseLerp(0.0f, model.ReloadCooldown, model.CurrentReload);
    }
}