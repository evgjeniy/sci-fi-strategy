using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Buildings.UI;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.ResourceSystems;
using SustainTheStrain.Scriptable.Buildings;
using SustainTheStrain.Scriptable.EnergySettings;

namespace SustainTheStrain.Buildings
{
    public class BuildingSystem : MonoEnergySystem
    {
        private IBuildingViewer _transparentBuildingViewer;
        private IBuildingCreateMenu _buildingCreateMenu;
        private IBuildingManagementMenu _buildingManagementMenu;

        private ISelectableInput<BuildingPlaceholder> _input;
        private Building.Factory _buildingFactory;
        private ResourceManager _resourceManager;
        private BuildingSystemSettings _buildingSettings;

        private BuildingPlaceholder _currentPlaceholder;

        public float DamageMultiplier => _buildingSettings.DamageMultipliers[CurrentEnergy];
        public float CooldownMultiplier => _buildingSettings.CooldownMultipliers[CurrentEnergy];

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
            _currentPlaceholder = placeholder;

            if (_currentPlaceholder.HasBuilding)
            {
                _buildingManagementMenu.Show(_currentPlaceholder);
            }
            else
            {
                _buildingCreateMenu.Show(_currentPlaceholder);
                _transparentBuildingViewer.Show(_currentPlaceholder);
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
            if (_currentPlaceholder.HasBuilding) return;
            if (_resourceManager.CurrentGold < buildingData.CreatePrice) return;

            _buildingCreateMenu.Hide(_currentPlaceholder);
            _transparentBuildingViewer.Hide(_currentPlaceholder);

            var newBuilding = _buildingFactory.Create(buildingData);

            _currentPlaceholder.SetBuilding(newBuilding);

            _resourceManager.CurrentGold -= buildingData.CreatePrice;

            _buildingManagementMenu.Show(_currentPlaceholder);
        }

        private void UpgradeBuilding()
        {
            if (!_currentPlaceholder.HasBuilding) return;
            if (_resourceManager.CurrentGold < _currentPlaceholder.Building.UpgradePrice) return;

            _currentPlaceholder.Building.CurrentUpgradeLevel++;

            _resourceManager.CurrentGold -= _currentPlaceholder.Building.UpgradePrice;
        }

        private void DestroyBuilding()
        {
            if (!_currentPlaceholder.HasBuilding) return;

            _buildingManagementMenu.Hide(_currentPlaceholder);

            _resourceManager.CurrentGold += _currentPlaceholder.Building.DestroyCompensation;
            _currentPlaceholder.DestroyBuilding();

            _buildingCreateMenu.Show(_currentPlaceholder);
        }

        #region Energy

        public override void SetEnergySettings(EnergySystemSettings settings)
        {
            _buildingSettings = settings as BuildingSystemSettings;
        }
        
        
        

        #endregion
    }
}