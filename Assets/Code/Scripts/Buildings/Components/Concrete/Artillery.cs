using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.ResourceSystems;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class Artillery : MonoBehaviour, ITurret
    {
        private IUpdatableState<Artillery> _currentState = new ArtilleryIdleState();
        private IResourceManager _resourceManager;
        private Observable<ArtilleryBuildingConfig> _config;
        private Observable<Vector3> _orientation;
        private Observable<SelectionType> _selection;

        public Area<Damageble> Area { get; } = new(conditions: damageable => damageable.Team != Team.Player);
        public Timer Timer { get; private set; }
        public int AttackCounter { get; set; }
        public ArtillerySystem EnergySystem { get; set; }
        public ISpawnPointProvider SpawnPointProvider { get; set; }

        public ArtilleryBuildingConfig Config => _config.Value;
        BuildingConfig IBuilding.Config => Config;

        public Vector3 Orientation
        {
            get => _orientation.Value;
            set => _orientation.Value = value;
        }

        [Inject]
        private void Construct(Timer timer, IResourceManager resourceManager,
            ArtillerySystem artillerySystem,
            Observable<ArtilleryBuildingConfig> config,
            Observable<Vector3> orientation,
            Observable<SelectionType> selection)
        {
            EnergySystem = artillerySystem;
            _resourceManager = resourceManager;
            _config = config;
            _selection = selection;
            _orientation = orientation;
            
            Timer = timer;
            Timer.ResetTime(config.Value.Cooldown);
        }

        private void Update() => _currentState = _currentState.Update(this);

        public void OnPointerEnter() => _selection.Value = SelectionType.Pointer;
        public void OnPointerExit() => _selection.Value = SelectionType.None;
        public void OnSelected() => _selection.Value = SelectionType.Select;
        public void OnDeselected() => _selection.Value = SelectionType.None;

        public void Upgrade()
        {
            if (_config.Value.NextLevelConfig == null) return;
            if (_resourceManager.TrySpend(_config.Value.NextLevelPrice) is false) return;

            _config.Value = _config.Value.NextLevelConfig;
        }

        public void Destroy()
        {
            GetComponentInParent<IPlaceholder>().IfNotNull(placeholder =>
            {
                placeholder.DestroyBuilding();
                _resourceManager.Gold.Value += _config.Value.Compensation;
            });
        }
    }
}