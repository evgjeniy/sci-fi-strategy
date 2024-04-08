using SustainTheStrain.Abilities;
using SustainTheStrain.Buildings.States;
using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.Input;
using SustainTheStrain.ResourceSystems;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Spawners;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    [RequireComponent(typeof(Outline))]
    public class Barrack : MonoBehaviour, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingFactory _buildingFactory;

        private BarrackManagementMenu _managementMenu;
        private GameObject _currentGfx;
        private IUpdatableState<Barrack> _currentState = new BarrackIdleState();

        public BarrackData Data { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder, IResourceManager resourceManager,
            IConfigProviderService configProvider, IBuildingFactory buildingFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _buildingFactory = buildingFactory;

            Data = new BarrackData
            (
                startConfig: configProvider.GetBuildingConfig<BarrackBuildingConfig>(),
                outline: GetComponent<Outline>(),
                recruitGroup: GetComponentInChildren<RecruitGroup>(),
                recruitSpawner: GetComponentInChildren<RecruitSpawner>(),
                radiusVisualizer: GetComponentInChildren<IZoneVisualizer>()
            );
        }

        private void Start()
        {
            var spawnPosition = _placeholder.Road.Project(_placeholder.transform.position) + Vector3.up * 2.5f;
            Data.RecruitSpawner.SpawnPosition = spawnPosition;
            Data.RecruitGroup.GuardPost.Position = spawnPosition;
        }

        private void OnEnable() => Data.Config.Changed += UpgradeGraphics;
        private void OnDisable() => Data.Config.Changed -= UpgradeGraphics;
        private void Update() => _currentState = _currentState.Update(this);

        public void OnPointerEnter() => Data.Outline.Enable();
        public void OnPointerExit() => Data.Outline.Disable();

        public void OnSelected()
        {
            if (_managementMenu == null)
                _managementMenu = _buildingFactory.CreateMenu<BarrackManagementMenu>(this);
            
            _managementMenu.Enable();
            Data.RadiusVisualizer.Radius = Data.Config.Value.Radius;
        }

        public void OnDeselected()
        {
            _managementMenu.Disable();

#if UNITY_EDITOR
            if (Const.IsDebugRadius) return;
#endif

            Data.RadiusVisualizer.Radius = 0;

            Data.RecruitsPointer.IfNotNull(x => x.DestroyObject());
        }

        public IInputState OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (Data.RecruitsPointer == null)
                return currentState;

            var pointerPosition = Data.RecruitsPointer.transform.position;
            Data.RecruitGroup.GuardPost.Position = pointerPosition;
            Data.RecruitSpawner.SpawnPosition = pointerPosition;
            return new InputIdleState();
        }

        public IInputState OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (Data.RecruitsPointer == null)
                return currentState;

            if (Physics.Raycast(ray, out var hit, float.MaxValue, Data.Config.Value.Mask) is false)
                return currentState;

            var barrackPosition = transform.position;
            var directionToPoint = hit.point - barrackPosition;
            var distance = Mathf.Min(directionToPoint.magnitude, Data.Config.Value.Radius);

            Data.RecruitsPointer.transform.position = barrackPosition + directionToPoint.normalized * distance;
            return currentState;
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

        public void SetUnitsPointState()
        {
            Data.RecruitsPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere)
                .With(x => x.GetComponent<MeshRenderer>().material.color = Color.red)
                .With(x => x.SetParent(transform));;

            _managementMenu.Disable();
        }

        private void UpgradeGraphics(BarrackBuildingConfig config)
        {
            _currentGfx.IfNotNull(x => x.DestroyObject());
            _currentGfx = Instantiate(config.GfxPrefab, transform);
            
            Data.RadiusVisualizer.Radius = config.Radius;
        }
        
#if UNITY_EDITOR

        private void Awake() => Const.IsDebugRadius.Changed += OnDebugRadiusChanged;
        private void OnDestroy() => Const.IsDebugRadius.Changed -= OnDebugRadiusChanged;
        private bool IsDebugRadius => Const.IsDebugRadius;
        [NaughtyAttributes.Button, NaughtyAttributes.DisableIf(nameof(IsDebugRadius))] private void ShowDebugRadius() => Const.IsDebugRadius.Value = true;
        [NaughtyAttributes.Button, NaughtyAttributes.EnableIf(nameof(IsDebugRadius))] private void HideDebugRadius() => Const.IsDebugRadius.Value = false;
        private void OnDebugRadiusChanged(bool isDebugRadius) => Data.RadiusVisualizer.Radius = isDebugRadius ? Data.Config.Value.Radius : Data.RadiusVisualizer.Radius;

#endif
    }
}