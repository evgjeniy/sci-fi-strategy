using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Extensions;
using UnityEngine.UI;

namespace SustainTheStrain.Tips
{
    [RequireComponent(typeof(Graphic))]
    public class HideByAnyButton : DotWeenGraphicAnimation, IPointerClickHandler
    {
        private Graphic _graphic;

        private void Awake()
        {
            if (Const.FirstGameStarted is false)
            {
                this.DestroyObject();
                return;
            }
            
            _graphic = GetComponent<Graphic>();
            Const.FirstGameStarted = false;
        }

        public void OnPointerClick(PointerEventData eventData) => Hide(_graphic);

        private void Update()
        {
            if (UnityEngine.Input.anyKey) 
                Hide(_graphic);
        }
    }
}
