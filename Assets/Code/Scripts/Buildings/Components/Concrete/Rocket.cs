using SustainTheStrain.Abilities;
using SustainTheStrain.Buildings.States;
using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.ResourceSystems;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class Rocket : MonoBehaviour, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingFactory _buildingFactory;

        private RocketManagementMenu _managementMenu;
        private BuildingRotator _currentGfx;
        private IUpdatableState<Rocket> _currentState = new RocketIdleState();

        public RocketData Data { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder, IResourceManager resourceManager,
            IConfigProviderService configProvider, IBuildingFactory buildingFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _buildingFactory = buildingFactory;

            Data = new RocketData
            (
                startConfig: configProvider.GetBuildingConfig<RocketBuildingConfig>(),
                outline: GetComponent<Outline>(),
                radiusVisualizer: transform.GetChild(0).GetComponent<IZoneVisualizer>(),
                sectorVisualizer: transform.GetChild(1).GetComponent<IZoneVisualizer>(),
                startOrientation: placeholder.Road.Project(placeholder.transform.position)
            );
        }

        private void Awake() { Data.Orientation.Changed += OrientationOnChanged; Const.IsDebugRadius.Changed += OnDebugRadiusChanged; }
        private void OnDestroy() { Data.Orientation.Changed -= OrientationOnChanged; Const.IsDebugRadius.Changed -= OnDebugRadiusChanged; }
        private void OnEnable() => Data.Config.Changed += UpgradeGraphics;
        private void OnDisable() => Data.Config.Changed -= UpgradeGraphics;
        private void Update() => _currentState = _currentState.Update(this);

        public void OnPointerEnter() => Data.Outline.Enable();
        public void OnPointerExit() => Data.Outline.Disable();

        public void OnSelected()
        {
            if (_managementMenu == null)
                _managementMenu = _buildingFactory.CreateMenu<RocketManagementMenu>(this);
            
            _managementMenu.Enable();
            Data.RadiusVisualizer.Radius = Data.SectorVisualizer.Radius = Data.Config.Value.Radius;
        }

        public void OnDeselected()
        {
            _managementMenu.Disable();

#if UNITY_EDITOR
            if (Const.IsDebugRadius) return;
#endif

            Data.RadiusVisualizer.Radius = Data.SectorVisualizer.Radius = 0;
        }

        public void Upgrade()
        {
            if (Data.Config.Value.NextLevelConfig == null) return;
            if (_resourceManager.TrySpend(Data.Config.Value.NextLevelPrice) is false) return;

            Data.Config.Value = Data.Config.Value.NextLevelConfig;
        }

        public void Destroy()
        {
            _placeholder.DestroyBuilding();
            _resourceManager.Gold.Value += Data.Config.Value.Compensation;
        }

        private void UpgradeGraphics(RocketBuildingConfig config)
        {
            _currentGfx.IfNotNull(x => x.DestroyObject());
            _currentGfx = _buildingFactory.CreateGfx(config.GfxPrefab, transform, Data.Orientation);
            Data.ProjectileSpawnPoint = _currentGfx.ProjectileSpawnPoint;

            Data.RadiusVisualizer.Radius = Data.SectorVisualizer.Radius = config.Radius;
            Data.SectorVisualizer.Angle = config.SectorAngle;
        }

        private void OrientationOnChanged(Vector3 orientation)
        {
            var lookRotation = Quaternion.LookRotation(orientation - transform.position);
            Data.SectorVisualizer.Direction = lookRotation.eulerAngles.y;
        }
        
#if UNITY_EDITOR

        private bool IsDebugRadius => Const.IsDebugRadius;
        [NaughtyAttributes.Button, NaughtyAttributes.DisableIf(nameof(IsDebugRadius))] private void ShowDebugRadius() => Const.IsDebugRadius.Value = true;
        [NaughtyAttributes.Button, NaughtyAttributes.EnableIf(nameof(IsDebugRadius))] private void HideDebugRadius() => Const.IsDebugRadius.Value = false;
        private void OnDebugRadiusChanged(bool isDebugRadius) => Data.RadiusVisualizer.Radius = isDebugRadius ? Data.Config.Value.Radius : Data.RadiusVisualizer.Radius;

#endif
    }
}