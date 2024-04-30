using System;
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
    public class Barrack : MonoBehaviour, IBuilding
    {
        [field: SerializeField] public RecruitGroup RecruitGroup { get; private set; }
        [field: SerializeField] public RecruitSpawner RecruitSpawner { get; private set; } 

        private IUpdatableState<Barrack> _currentState = new BarrackInitState();
        private IResourceManager _resourceManager;
        private Observable<BarrackBuildingConfig> _config;
        private Observable<Vector3> _spawnPoint;
        private Observable<SelectionType> _selection;
        private GameObject _recruitsPointer;

        public Timer Timer { get; private set; }

        public BarrackBuildingConfig Config => _config.Value;
        BuildingConfig IBuilding.Config => Config;

        public Vector3 SpawnPoint
        {
            get => RecruitGroup.GuardPost.Position;
            set => RecruitGroup.GuardPost.Position = RecruitSpawner.SpawnPosition = value;
        }

        [Inject]
        private void Construct(Timer timer, IResourceManager resourceManager,
            Observable<BarrackBuildingConfig> config,
            Observable<Vector3> spawnPoint,
            Observable<SelectionType> selection)
        {
            _resourceManager = resourceManager;
            _selection = selection;

            _config = config;
            _config.Changed += barrackConfig =>
            {
                if (barrackConfig.HasPassiveSkill is false) return;
                
                if (barrackConfig.IsMaxEnergy)
                    barrackConfig.PassiveSkill.EnableSkill(gameObject);
                else 
                    barrackConfig.PassiveSkill.DisableSkill(gameObject);
            };

            SpawnPoint = spawnPoint.Value + Vector3.up * 2f;
            
            Timer = timer;
            Timer.ResetTime(config.Value.RespawnCooldown);
        }

        private void Update() => _currentState = _currentState.Update(this);

        public void OnPointerEnter() => _selection.Value = SelectionType.Pointer;
        public void OnPointerExit() => _selection.Value = SelectionType.None;
        public void OnSelected() => _selection.Value = SelectionType.Select;
        public void OnDeselected() { _selection.Value = SelectionType.None; _recruitsPointer.IfNotNull(x => x.DestroyObject()); }

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

        public IInputState OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (_recruitsPointer == null)
                return currentState;

            SpawnPoint = _recruitsPointer.transform.position;
            return new InputIdleState();
        }

        public IInputState OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (_recruitsPointer == null)
                return currentState;

            if (Physics.Raycast(ray, out var hit, float.MaxValue, Config.Mask) is false)
                return currentState;

            var barrackPosition = transform.position;
            var directionToPoint = hit.point - barrackPosition;
            var distance = Mathf.Min(directionToPoint.magnitude, Config.Radius);

            _recruitsPointer.transform.position = barrackPosition + directionToPoint.normalized * distance;
            return currentState;
        }

        public void SetUnitsPointState()
        {
            var zoneVisualizer = _config.Value.RecruitSpawnAimPrefab.Spawn();
            _recruitsPointer = zoneVisualizer.gameObject.With(x => x.SetParent(transform));
        }
    }
}