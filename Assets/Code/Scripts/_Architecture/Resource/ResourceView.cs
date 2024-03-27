using UnityEngine;

namespace SustainTheStrain._Architecture.Resource
{
    public class ResourceView : MonoBehaviour, IView<ResourceData>
    {
        [SerializeField] private TMPro.TMP_Text _text;

        public void Display(ResourceData model)
        {
            _text.text = $"MONEY: {model.Money}";
        }
    }
}