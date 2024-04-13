using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public static class UIExtensions
    {
        public static void LookAtCamera(this Transform uiTransform, Transform from, float positionLerpOffset = 0.1f)
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
    }
}