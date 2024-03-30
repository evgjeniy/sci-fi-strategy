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

            var lookDirection = fromPosition - cameraPosition;

            uiTransform.position = cameraPosition + lookDirection * (1 - positionLerpOffset);
            uiTransform.LookAt(uiTransform.position + lookDirection, worldCamera.transform.up + Vector3.up);
        }
    }
}