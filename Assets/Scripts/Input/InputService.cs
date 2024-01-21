using System;
using System.Collections.Generic;
using SustainTheStrain.Buildings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace SustainTheStrain.Input
{
    public class InputService : IBuildingInputService, IInitializable, IDisposable
    {
        [Serializable]
        public class Settings
        {
            [field: SerializeField] public EventSystem EventSystem { get; private set; }
            [field: SerializeField] public LayerMask RayCastMask { get; private set; } = 255;
            [field: SerializeField] public float MaxDistance { get; private set; } = float.MaxValue;
        }

        private readonly Settings _settings;
        private readonly InputActions _actions = new();
        private readonly List<RaycastResult> _rayCastResults = new();

        private Vector2 _mousePosition;
        private Camera _camera;
        private BuildingPlaceholder _placeholder;
        private bool _isPointerUnderUI;

        public event Action<Vector2> OnLeftMouseClick;
        public event Action<BuildingPlaceholder> OnPlaceholderPointerEnter;
        public event Action<BuildingPlaceholder> OnPlaceholderPointerExit;
        public event Action<BuildingPlaceholder> OnPlaceholderPointerLeftClick;

        public InputService(Settings settings) => _settings = settings;

        public void Enable() => _actions.Enable();
        public void Disable() => _actions.Disable();

        public void Initialize()
        {
            _camera = Camera.main;
            
            Enable();
            _actions.Mouse.MousePosition.performed += OnMouseMoved;
            _actions.Mouse.LeftButton.performed += OnLeftMouseButton;
        }

        public void Dispose()
        {
            _actions.Mouse.MousePosition.performed -= OnMouseMoved;
            _actions.Mouse.LeftButton.performed -= OnLeftMouseButton;
            Disable();
        }

        private void OnMouseMoved(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
            
            _isPointerUnderUI = IsPointerUnderUI();
            if (_isPointerUnderUI) return;

            var ray = _camera.ScreenPointToRay(_mousePosition);
            if (!Physics.Raycast(ray, out var hit, _settings.MaxDistance, _settings.RayCastMask.value)) return;
            
            if (IsPointerUnderBuildingPlaceholder(hit.collider)) return;
            if (/*HandleMovement(hit)*/ false) return;
        }

        private void OnLeftMouseButton(InputAction.CallbackContext context)
        {
            if (_isPointerUnderUI) return;
            
            if (_placeholder != null)
                OnPlaceholderPointerLeftClick?.Invoke(_placeholder);
            else
                OnLeftMouseClick?.Invoke(_mousePosition);
        }

        private bool IsPointerUnderBuildingPlaceholder(Component hitCollider)
        {
            var hasPlaceholder = hitCollider.TryGetComponent<BuildingPlaceholder>(out var placeholder);
            if (hasPlaceholder)
            {
                if (_placeholder != null) return true;
                
                OnPlaceholderPointerEnter?.Invoke(placeholder);
                _placeholder = placeholder;
            }
            else
            {
                if (_placeholder == null) return false;
                
                OnPlaceholderPointerExit?.Invoke(_placeholder);
                _placeholder = null;
            }
            return hasPlaceholder;
        }

        private bool IsPointerUnderUI()
        {
            var pointerEventData = new PointerEventData(_settings.EventSystem) { position = _mousePosition };
            _settings.EventSystem.RaycastAll(pointerEventData, _rayCastResults);
            return _rayCastResults.Count != 0;
        }
    }
}