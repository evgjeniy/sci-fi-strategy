using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SustainTheStrain.Input
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

        public static bool IsPointerUnder<T>(this IInputData inputData, out T component)
        {
            component = default;

            return inputData.MousePosition.TryGetCameraRay(out var screenRay) &&
                   screenRay.TryGetComponent(inputData, out component);
        }

        public static bool TryGetCameraRay(this Vector2 mousePosition, out Ray screenRay)
        {
            screenRay = default;

            var camera = Camera.main;
            if (camera == null) return false;

            screenRay = camera.ScreenPointToRay(mousePosition);
            return true;
        }

        public static bool TryGetComponent<T>(this Ray ray, IInputData inputData, out T component)
        {
            component = default;

            return Physics.Raycast(ray, out var hit, inputData.Distance, inputData.Mask) && 
                   hit.collider.TryGetComponent(out component);
        }
    }
}