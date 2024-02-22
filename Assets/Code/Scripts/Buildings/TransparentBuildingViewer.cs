using Cysharp.Threading.Tasks;
using DG.Tweening;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public interface IBuildingViewer
    {
        public void Show(BuildingPlaceholder placeholder);
        public void Hide(BuildingPlaceholder placeholder);
        public void ChangeBuildingMeshPreview(BuildingData buildingData);
    }

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class TransparentBuildingViewer : MonoBehaviour, IBuildingViewer
    {
        [SerializeField, Range(0.0f, 5.0f)] private float _scaleAnimationDuration = 0.5f;

        private Sequence _tween;
        private Transform _cashedTransform;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _cashedTransform = transform;
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnDestroy() => _tween?.Kill();

        public async void ChangeBuildingMeshPreview(BuildingData buildingData)
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

        public async void Show(BuildingPlaceholder placeholder)
        {
            if (placeholder.HasBuilding) return;
            
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

        public async void Hide(BuildingPlaceholder placeholder)
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