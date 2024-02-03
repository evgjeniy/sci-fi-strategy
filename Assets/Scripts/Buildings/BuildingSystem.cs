using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Buildings.UI.Menus;
using SustainTheStrain.Input;
using SustainTheStrain.ResourceSystems;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class BuildingSystem : MonoBehaviour
    {
        private IBuildingViewer _transparentBuildingViewer;
        private IBuildingCreateMenu _buildingCreateMenu;
        private IBuildingManagementMenu _buildingManagementMenu;

        private ISelectableInput<BuildingPlaceholder> _input;
        private Building.Factory _buildingFactory;
        private ResourceManager _resourceManager;

        private BuildingPlaceholder CurrentPlaceholder { get; set; }

        [Zenject.Inject]
        private void Construct(ISelectableInput<BuildingPlaceholder> input, Building.Factory buildingFactory, ResourceManager resourceManager)
        {
            _input = input;
            _buildingFactory = buildingFactory;
            _resourceManager = resourceManager;
        }

        private void Awake()
        {
            _transparentBuildingViewer = GetComponentInChildren<IBuildingViewer>();
            _buildingCreateMenu = GetComponentInChildren<IBuildingCreateMenu>();
            _buildingManagementMenu = GetComponentInChildren<IBuildingManagementMenu>();
        }

        private void OnEnable()
        {
            _input.OnSelected += OnPlaceholderSelected;
            _input.OnDeselected += OnPlaceholderDeselected;
            
            _buildingCreateMenu.OnCreateRequested += CreateBuilding;
            _buildingCreateMenu.OnBuildingDataChanged += _transparentBuildingViewer.ChangeBuildingMeshPreview;
            
            _buildingManagementMenu.OnUpgradeRequested += UpgradeBuilding;
            _buildingManagementMenu.OnDestroyRequested += DestroyBuilding;
        }

        private void OnDisable()
        {
            _input.OnSelected -= OnPlaceholderSelected;
            _input.OnDeselected -= OnPlaceholderDeselected;
            
            _buildingCreateMenu.OnCreateRequested -= CreateBuilding;
            _buildingCreateMenu.OnBuildingDataChanged -= _transparentBuildingViewer.ChangeBuildingMeshPreview;

            _buildingManagementMenu.OnUpgradeRequested -= UpgradeBuilding;
            _buildingManagementMenu.OnDestroyRequested -= DestroyBuilding;
        }

        private void OnPlaceholderSelected(BuildingPlaceholder placeholder)
        {
            CurrentPlaceholder = placeholder;

            if (CurrentPlaceholder.HasBuilding)
            {
                _buildingManagementMenu.Show(CurrentPlaceholder);
            }
            else
            {
                _buildingCreateMenu.Show(CurrentPlaceholder);
                _transparentBuildingViewer.Show(CurrentPlaceholder);
            }
        }

        private void OnPlaceholderDeselected(BuildingPlaceholder placeholder)
        {
            _buildingCreateMenu.Hide(placeholder);
            _buildingManagementMenu.Hide(placeholder);
            _transparentBuildingViewer.Hide(placeholder);
        }

        private void CreateBuilding(BuildingData buildingData)
        {
            if (CurrentPlaceholder.HasBuilding) return;
            if (_resourceManager.CurrentGold < buildingData.CreatePrice) return;
            
            _buildingCreateMenu.Hide(CurrentPlaceholder);
            _transparentBuildingViewer.Hide(CurrentPlaceholder);
            
            var newBuilding = _buildingFactory.Create(buildingData);
            CurrentPlaceholder.SetBuilding(newBuilding);

            _resourceManager.CurrentGold -= buildingData.CreatePrice;

            _buildingManagementMenu.Show(CurrentPlaceholder);
        }

        private void UpgradeBuilding()
        {
            if (!CurrentPlaceholder.HasBuilding) return;
            if (_resourceManager.CurrentGold < CurrentPlaceholder.Building.UpgradePrice) return;

            CurrentPlaceholder.Building.CurrentUpgradeLevel++;

            _resourceManager.CurrentGold -= CurrentPlaceholder.Building.UpgradePrice;
        }
        
        private void DestroyBuilding()
        {
            if (!CurrentPlaceholder.HasBuilding) return;
            
            _buildingManagementMenu.Hide(CurrentPlaceholder);

            _resourceManager.CurrentGold += CurrentPlaceholder.Building.DestroyCompensation;
            CurrentPlaceholder.DestroyBuilding();

            _buildingCreateMenu.Show(CurrentPlaceholder);
        }
    }
}