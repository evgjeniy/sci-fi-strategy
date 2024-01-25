using Cysharp.Threading.Tasks;
using DG.Tweening;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Input;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class TransparentBuildingViewer : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 5.0f)] private float _scaleAnimationDuration = 0.5f;
        
        private Sequence _tween;
        private Transform _cashedTransform;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private IBuildingInputService _input;
        private IBuildingSelector _buildingSelector;
        private IBuildingSystem _buildingSystem;

        [Zenject.Inject]
        private void Construct(
            IBuildingInputService buildingInput/*,
            IBuildingSelector buildingSelector,
            IBuildingSystem buildingSystem*/)
        {
            _input = buildingInput;
            // _buildingSelector = buildingSelector;
            // _buildingSystem = buildingSystem;

            _cashedTransform = transform;
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            _input.OnPlaceholderPointerLeftClick += ShowPreview;
            _input.OnLeftMouseClick += HidePreview;
        }

        private void OnDisable()
        {
            _input.OnPlaceholderPointerLeftClick -= ShowPreview;
            _input.OnLeftMouseClick -= HidePreview;
        }

        private void OnDestroy() => _tween?.Kill();
        
        private async void ChangeBuildingMeshPreview(BuildingData buildingData)
        {
            var halfDuration = _scaleAnimationDuration / 2.0f;
            
            _tween?.Kill();
            _tween = DOTween.Sequence()
                .Append(_cashedTransform.DOScale(Vector3.zero, halfDuration))
                .Append(_cashedTransform.DOScale(Vector3.one, halfDuration).OnStart(() =>
                {
                    _meshFilter.mesh = buildingData.Mesh;
                }));

            await _tween.Play().ToUniTask();
        } 

        private async void ShowPreview(BuildingPlaceholder placeholder)
        {
            _cashedTransform.localScale = Vector3.zero;
            _cashedTransform.position = placeholder.BuildingRoot.position;

            _tween?.Kill();
            _tween = DOTween.Sequence().Append
            (
                _cashedTransform
                    .DOScale(Vector3.one, _scaleAnimationDuration)
                    .OnStart(_meshRenderer.Enable)
                    .SetEase(Ease.OutBounce)
            );
            
            await _tween.Play().ToUniTask();
        }
        
        private async void HidePreview(Vector2 vector2)
        {
            _cashedTransform.localScale = Vector3.one;

            _tween?.Kill();
            _tween = DOTween.Sequence().Append
            (
                _cashedTransform
                    .DOScale(Vector3.zero, _scaleAnimationDuration)
                    .OnKill(_meshRenderer.Disable)
                    .SetEase(Ease.OutExpo)
            );
            
            await _tween.Play().ToUniTask();
        }
    }
}