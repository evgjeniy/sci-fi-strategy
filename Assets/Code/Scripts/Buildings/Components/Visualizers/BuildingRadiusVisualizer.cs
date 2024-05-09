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

            //Const.IsDebugRadius.Changed += OnDebugRadiusChanged;
        }

        protected virtual void OnDestroy()
        {
            _config.Changed -= UpdateConfig;
            _buildingSelected.Changed -= UpdateSelection;
            
            //Const.IsDebugRadius.Changed -= OnDebugRadiusChanged;
        }
        
        protected virtual void UpdateSelection(SelectionType selectionType)
        {
            if (Const.IsDebugRadius.Value)
                gameObject.SetActive(true);
            else 
                gameObject.SetActive(selectionType == SelectionType.Select);
        }

        protected virtual void UpdateConfig(TBuildingConfig config) => Radius = config.Radius;
        
        
#if UNITY_EDITOR

        private bool IsDebugRadius => Const.IsDebugRadius;
        [NaughtyAttributes.Button, NaughtyAttributes.DisableIf(nameof(IsDebugRadius))] private void ShowDebugRadius() => Const.IsDebugRadius.Value = true;
        [NaughtyAttributes.Button, NaughtyAttributes.EnableIf(nameof(IsDebugRadius))] private void HideDebugRadius() => Const.IsDebugRadius.Value = false;
        private void OnDebugRadiusChanged(bool isDebugRadius) => UpdateSelection(_buildingSelected.Value);

#endif
    }
}