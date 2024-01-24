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

        private ISelectableInput<BuildingPlaceholder> _input;

        // private IBuildingSelector _buildingSelector;
        // private IBuildingSystem _buildingSystem;
        private RaycastHit _hit;

        [Zenject.Inject]
        private void Construct(
            ISelectableInput<BuildingPlaceholder> buildingInput /*,
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
            _input.OnSelected += ShowPreview;
            _input.OnDeselected += HidePreview;
        }

        private void OnDisable()
        {
            _input.OnSelected -= ShowPreview;
            _input.OnDeselected -= HidePreview;
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

        private async void HidePreview(BuildingPlaceholder buildingPlaceholder)
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