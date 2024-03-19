using UnityEngine;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public static class UIExtensions
    {
        public static void LookAtCamera(this Transform uiTransform, float positionLerpOffset = 0.15f)
        {
            var worldCamera = Camera.main;
            if (worldCamera == null) return;

            var cameraTransform = worldCamera.transform;
            var uiPosition = uiTransform.position;

            uiTransform.position = Vector3.Lerp(uiPosition, cameraTransform.position, positionLerpOffset);
            uiTransform.LookAt(uiPosition - cameraTransform.position, cameraTransform.up);
        }
    }
}