using System;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.ResourceSystems;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class Rocket : MonoBehaviour, ITurret
    {
        private IUpdatableState<Rocket> _currentState = new RocketIdleState();
        private IResourceManager _resourceManager;
        private Observable<RocketBuildingConfig> _config;
        private Observable<Vector3> _orientation;
        private Observable<SelectionType> _selection;

        public Area<Damageble> Area { get; } = new(conditions: damageable => damageable.Team != Team.Player);
        public Timer Timer { get; private set; }
        public ISpawnPointProvider SpawnPointProvider { get; set; }

        public RocketBuildingConfig Config => _config.Value;
        BuildingConfig IBuilding.Config => Config;

        public Vector3 Orientation
        {
            get => _orientation.Value;
            set => _orientation.Value = value;
        }

        [Inject]
        private void Construct(Timer timer, IResourceManager resourceManager,
            Observable<RocketBuildingConfig> config,
            Observable<Vector3> orientation,
            Observable<SelectionType> selection)
        {
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