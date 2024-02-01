using System.Collections;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.UI.Menus
{
    public abstract class BuildingMenu : MonoBehaviour
    {
        [SerializeField] private Canvas attachedCanvas;
        
        private Coroutine _coroutine;

        public void Show(BuildingPlaceholder placeholder)
        {
            attachedCanvas.Activate();
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(UpdateSelectionMenuPosition(placeholder.transform));
        }

        public void Hide(BuildingPlaceholder placeholder)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            attachedCanvas.Deactivate();
        }

        private IEnumerator UpdateSelectionMenuPosition(Transform placeholder)
        {
            var mainCamera = Camera.main.transform;

            while (true)
            {
                var placeholderPosition = placeholder.position;
                var mainCameraPosition = mainCamera.position;

                transform.position = Vector3.Lerp(placeholderPosition, mainCameraPosition, 0.15f);
                transform.LookAt(placeholderPosition - mainCameraPosition);
                yield return null;
            }
        }
    }
}