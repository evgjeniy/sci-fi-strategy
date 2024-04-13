using SustainTheStrain.Configs.Buildings;

namespace SustainTheStrain.Buildings
{
    public class BuildingRadiusVisualizer<TBuildingConfig> : ZoneVisualizer where TBuildingConfig : BuildingConfig
    {
        private IObservable<TBuildingConfig> _config;
        private IObservable<SelectionType> _buildingSelected;

        [Zenject.Inject]
        private void Construct(IObservable<TBuildingConfig> config, IObservable<SelectionType> buildingSelected)
        {
            _buildingSelected = buildingSelected;
            _config = config;
        }

        protected override void Awake()
        {
            base.Awake();
            _config.Changed += UpdateConfig;
            _buildingSelected.Changed += UpdateSelection;
        }

        protected virtual void OnDestroy()
        {
            _config.Changed -= UpdateConfig;
            _buildingSelected.Changed -= UpdateSelection;
        }
        
        protected virtual void UpdateSelection(SelectionType selectionType) => gameObject.SetActive(selectionType == SelectionType.Select);
        protected virtual void UpdateConfig(TBuildingConfig config) => Radius = config.Radius;
    }
}