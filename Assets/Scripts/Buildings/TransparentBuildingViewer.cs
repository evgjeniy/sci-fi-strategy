using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class TransparentBuildingViewer : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 5.0f)] private float _scaleAnimationDuration = 0.5f;
        
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        // private BuildingSelector _buildingSelector;
        private BuildingSystem _buildingSystem;
        private TweenerCore<Vector3,Vector3,VectorOptions> _tween;

        [Inject]
        private void Construct(BuildingSystem buildingSystem /*, BuildingSelector buildingSelector */)
        {
            _buildingSystem = buildingSystem;
            // _buildingSelector = buildingSelector;
            
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            _buildingSystem.OnMouseClick += HideBuildingPreview;
            // buildingSelector.OnSelectionEnded += HideBuildingPreview;
            // buildingSelector.OnBuildingChanged += ChangeBuildingMeshPreview;
            // buildingSelector.OnSelected += HideBuildingPreview;
            
            _buildingSystem.OnMousePlaceholderClick += ShowBuildingPreview;
        }

        private void OnDisable()
        {
            _buildingSystem.OnMouseClick -= HideBuildingPreview;
            // Similar unsubscribing
            
            _buildingSystem.OnMousePlaceholderClick -= ShowBuildingPreview;
        }

        private async void ChangeBuildingMeshPreview(/*BuildingData buildingData*/)
        {
            // var mesh = buildingData.mesh;

            var halfDuration = _scaleAnimationDuration / 2.0f;
            var sequence = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.zero, halfDuration))
                .Append(transform.DOScale(Vector3.one, halfDuration)/*.OnStart(_meshFilter.mesh = buildingData.mesh)*/);

            await sequence.Play().ToUniTask();
        } 

        private async void ShowBuildingPreview(BuildingPlaceholder placeholder)
        {
            transform.localScale = Vector3.zero;
            transform.position = placeholder.transform.position;

            _tween?.Kill();
            _tween = transform.DOScale(Vector3.one, _scaleAnimationDuration).OnStart(_meshRenderer.Enable).SetEase(Ease.OutBounce);
            await _tween.Play().ToUniTask();
        }

        private async void HideBuildingPreview()
        {
            transform.localScale = Vector3.one;

            _tween?.Kill();
            _tween = transform.DOScale(Vector3.zero, _scaleAnimationDuration).OnKill(_meshRenderer.Disable).SetEase(Ease.OutExpo);
            await _tween.Play().ToUniTask();
        }
    }
}