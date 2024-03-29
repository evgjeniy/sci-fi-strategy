using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SustainTheStrain._Contracts
{
    public static class InputExtensions
    {
        private static readonly List<RaycastResult> RayCastResults = new();

        public static bool IsPointerUnderUI(this Vector2 mousePosition)
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null) return false;

            eventSystem.RaycastAll(new PointerEventData(eventSystem) { position = mousePosition }, RayCastResults);
            return RayCastResults.Count != 0;
        }

        public static bool TryGetCameraRay(this Vector2 mousePosition, out Ray screenRay)
        {
            screenRay = default;

            var camera = Camera.main;
            if (camera == null) return false;

            screenRay = camera.ScreenPointToRay(mousePosition);
            return true;
        }

        public static bool TryGetRaycastComponent<T>(this Ray ray, InputSettings settings, out T component)
        {
            component = default;

            return Physics.Raycast(ray, out var hit, settings.Distance, settings.Mask) && 
                   hit.collider.TryGetComponent(out component);
        }
    }
}