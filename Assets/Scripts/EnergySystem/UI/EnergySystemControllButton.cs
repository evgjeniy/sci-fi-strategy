using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SustainTheStrain.EnergySystem
{
    public class EnergySystemControllButton : Button
    {
        public event Action OnLeftMouseClick;
        public event Action OnRightMouseClick;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnLeftMouseClick?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    OnRightMouseClick?.Invoke();
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}