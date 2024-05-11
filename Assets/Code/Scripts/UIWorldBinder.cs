using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    public class UIWorldBinder : MonoBehaviour
    {
        [SerializeField]
        private Transform _worldAnchor;
        [SerializeField]
        private float _bounds;
        [SerializeField]
        private Vector2 _offset;

        private RectTransform _rectTransform;

        private void OnEnable()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Update()
        {
            SetValidUIPosition();
        }

        public void SetValidUIPosition()
        {
            var screenPosition = Camera.main.WorldToScreenPoint(_worldAnchor.position, Camera.MonoOrStereoscopicEye.Mono);

            screenPosition = new Vector2(
                Mathf.Clamp(screenPosition.x + _offset.x, _bounds, Camera.main.scaledPixelWidth - _bounds),
                Mathf.Clamp(screenPosition.y + _offset.y, _bounds, Camera.main.scaledPixelHeight - _bounds));

            _rectTransform.position = screenPosition;
        }
    }
}
