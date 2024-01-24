using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SustainTheStrain.Input
{
    public static class InputExtensions
    {
        private static readonly List<RaycastResult> RayCastResults = new();

        public static bool IsPointerUnderUI(this Vector2 mousePosition, EventSystem eventSystem)
        {
            var pointerEventData = new PointerEventData(eventSystem) { position = mousePosition };
            eventSystem.RaycastAll(pointerEventData, RayCastResults);
            return RayCastResults.Count != 0;
        }
    }
}