using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public static class UIExtensions
    {
        public static void LookAtCamera(this Transform uiTransform, Transform from, float positionLerpOffset = 0.15f)
        {
            var worldCamera = Camera.main;
            if (worldCamera == null) return;

            var cameraPosition = worldCamera.transform.position;
            var fromPosition = from.position;

            uiTransform.position = Vector3.Lerp(fromPosition, cameraPosition, positionLerpOffset);
            uiTransform.LookAt(fromPosition - cameraPosition, worldCamera.transform.up);
        }
    }
}