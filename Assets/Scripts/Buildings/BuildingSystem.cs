using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Input;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public interface IBuildingSystem
    {
        BuildingData SelectedData { get; }
    }

    public class BuildingSystem : MonoBehaviour, IBuildingSystem
    {
        [SerializeField] private TransparentBuildingViewer _transparentBuildingViewer;
        [SerializeField] private BuildingSelector _buildingSelector;
        
        private ISelectableInput<BuildingPlaceholder> _input;
        private LazyInject<Building.Factory> _buildingFactory;

        public BuildingData SelectedData => _buildingSelector.SelectedData;

        [Inject]
        private void Construct(ISelectableInput<BuildingPlaceholder> input, LazyInject<Building.Factory> buildingFactory)
        {
            _input = input;
            _buildingFactory = buildingFactory;
        }
        
        private void OnEnable()
        {
            _input.OnSelected += _transparentBuildingViewer.ShowPreview;
            _input.OnSelected += _buildingSelector.ShowSelectionMenu;
            _input.OnDeselected += _transparentBuildingViewer.HidePreview;
            _input.OnDeselected += _buildingSelector.HideSelectionMenu;
            
            _buildingSelector.OnCreate += CreateBuilding;
            _buildingSelector.OnBuildingDataChanged += _transparentBuildingViewer.ChangeBuildingMeshPreview;
        }

        private void OnDisable()
        {
            _input.OnSelected -= _transparentBuildingViewer.ShowPreview;
            _input.OnSelected -= _buildingSelector.ShowSelectionMenu;
            _input.OnDeselected -= _transparentBuildingViewer.HidePreview;
            _input.OnDeselected -= _buildingSelector.HideSelectionMenu;
            
            _buildingSelector.OnCreate -= CreateBuilding;
            _buildingSelector.OnBuildingDataChanged -= _transparentBuildingViewer.ChangeBuildingMeshPreview;
        }

        private void CreateBuilding(BuildingData buildingData)
        {
            var building = _buildingFactory.Value.Create();
            building.transform.position = _transparentBuildingViewer.transform.position;
        }
    }
}