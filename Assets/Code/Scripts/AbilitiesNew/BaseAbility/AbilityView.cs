using SustainTheStrain._Architecture;
using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain.AbilitiesNew
{
    public class AbilityView<TModel> : MonoBehaviour, IView<TModel> where TModel : AbilityData, IModel<TModel>
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public Slider Slider { get; private set; }

        public virtual void Display(TModel model) => Slider.value = Mathf.InverseLerp(0.0f, model.ReloadCooldown, model.CurrentReload);
    }
}