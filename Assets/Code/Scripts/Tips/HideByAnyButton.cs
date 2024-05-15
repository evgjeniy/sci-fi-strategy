using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SustainTheStrain.Tips
{
    [RequireComponent(typeof(Graphic))]
    public class HideByAnyButton : DotWeenGraphicAnimation, IPointerClickHandler
    {
        private Graphic _graphic;

        private void Awake() => _graphic = GetComponent<Graphic>();


        public void OnPointerClick(PointerEventData eventData) => Hide(_graphic);

        private void Update()
        {
            if (UnityEngine.Input.anyKey) 
                Hide(_graphic);
        }
    }
}
