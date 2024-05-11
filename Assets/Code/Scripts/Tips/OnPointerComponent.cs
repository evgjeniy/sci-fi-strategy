using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SustainTheStrain.Tips
{
    public class OnPointerComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent _onPointerEnter;
        [SerializeField] private UnityEvent _onPointerExit;

        private bool _isPointerEnter;
        
        public void OnPointerEnter(PointerEventData _)
        {
            if (_isPointerEnter) return;
            
            _isPointerEnter = true;
            _onPointerEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData _)
        {
            if (!_isPointerEnter) return;
            
            _isPointerEnter = false;
            _onPointerExit.Invoke();
        }
    }
}