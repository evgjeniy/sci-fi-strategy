using System.Globalization;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public static class UIExtensions
    {
        public static void LookAtCamera(this Transform uiTransform, Transform from, float positionLerpOffset = 0.35f)
        {
            var worldCamera = Camera.main;
            if (worldCamera == null) return;

            var cameraTransform = worldCamera.transform;

            if (worldCamera.orthographic)
            {
                var distanceToCamera = Vector3.Distance(from.position, cameraTransform.position);
                var lerpPosition = from.position - cameraTransform.forward * distanceToCamera;
                
                uiTransform.position = Vector3.Lerp(from.position, lerpPosition, positionLerpOffset);
                uiTransform.LookAt(uiTransform.position + cameraTransform.forward, cameraTransform.up);
            }
            else
            {
                uiTransform.position = Vector3.Lerp(from.position, cameraTransform.position, positionLerpOffset);
                uiTransform.LookAt(uiTransform.position + cameraTransform.forward, cameraTransform.up);
            }
        }
        
        public static string GetUpgradeString(this float firstNumber, float secondNumber)
        {
            var number = firstNumber - secondNumber;

            return number switch
            {
                > 0 => $"<color=\"green\">+{number:0.00}</color>",
                < 0 => $"<color=\"red\">-{number:0.00}</color>",
                _ => ""
            };
        }
        
        public static string GetUpgradeString(this int firstNumber, int secondNumber)
        {
            var number = firstNumber - secondNumber;

            return number switch
            {
                > 0 => $"<color=\"green\">+{number}</color>",
                < 0 => $"<color=\"red\">-{number}</color>",
                _ => ""
            };
        }
    }
}