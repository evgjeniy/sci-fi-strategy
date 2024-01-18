using System;
using SustainTheStrain.Input;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BuildingSystem : MonoBehaviour
    {
        [SerializeField] private float _sphereCastRadius = 0.5f;
        [SerializeField] private LayerMask _sphereCastLayerMask;
        [SerializeField] private float _maxSphereCastDistance = 100.0f;

        private BuildingPlaceholder _placeholder;

        private Camera _camera;
        private IInputService _inputService;

        public event Action OnMouseClick; // TODO: needs to be replaced at some 'BuildingSelector'
        public event Action<BuildingPlaceholder> OnMousePlaceholderClick;
        public event Action<BuildingPlaceholder> OnMousePlaceholderEnter;
        public event Action<BuildingPlaceholder> OnMousePlaceholderExit;
        
        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _inputService.OnLeftMouseClick += OnLeftButton;
            _inputService.OnMouseMove += OnMouseMove;
        }

        public void OnDisable()
        {
            _inputService.OnLeftMouseClick -= OnLeftButton;
            _inputService.OnMouseMove -= OnMouseMove;
        }

        private void OnLeftButton()
        {
            if (_placeholder == null)
                OnMouseClick?.Invoke();
            else
                OnMousePlaceholderClick?.Invoke(_placeholder);
        }

        private void OnMouseMove()
        {
            var screenPointToRay = _camera.ScreenPointToRay(_inputService.MousePosition);

            if (!CheckSphereCast(screenPointToRay, out var hit)) return;

            if (hit.collider.TryGetComponent<BuildingPlaceholder>(out var placeholder))
            {
                if (_placeholder != null) return;

                OnMousePlaceholderEnter?.Invoke(placeholder);
                _placeholder = placeholder;
            }
            else
            {
                if (_placeholder == null) return;
                
                OnMousePlaceholderExit?.Invoke(_placeholder);
                _placeholder = null;
            }
        }

        private bool CheckSphereCast(Ray ray, out RaycastHit hit) => Physics.SphereCast
        (
            ray, _sphereCastRadius, out hit, _maxSphereCastDistance, _sphereCastLayerMask
        );
    }
}