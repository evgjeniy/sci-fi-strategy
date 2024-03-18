using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.UI;

namespace SustainTheStrain._Contracts.Buildings
{
    [RequireComponent(typeof(RectTransform))]
    public class BuildingCreateMenu : MonoCashed<RectTransform>
    {
        [SerializeField, Range(0.0f, 1.0f)] private float _radiusMultiplier;
        
        private float Min => Mathf.Min(Cashed1.rect.height, Cashed1.rect.width);
        
        public void AddButton(Button button)
        {
            var radius = Min * _radiusMultiplier;
        }
    }
}