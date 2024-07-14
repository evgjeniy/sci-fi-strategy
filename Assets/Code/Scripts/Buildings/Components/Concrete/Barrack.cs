using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.EnergySystem;
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
        private Observable<SelectionType> _selection;
        private GameObject _recruitsPointer;

        public Timer[] Timers { get; private set; }
        public BarrackSystem BarrackSystem { get; private set; }

        public BarrackBuildingConfig Config => _config.Value;
        BuildingConfig IBuilding.Config => Config;

        public Vector3 SpawnPoint
        {
            get => RecruitGroup.GuardPost.Position;
            set => RecruitGroup.GuardPost.Position = RecruitSpawner.SpawnPosition = value;
        }

        [Inject]
        private void Construct(IResourceManager resourceManager,
            BarrackSystem barrackSystem,
            Observable<BarrackBuildingConfig> config,
            Observable<Vector3> spawnPoint,
            Observable<SelectionType> selection)
        {
            BarrackSystem = barrackSystem;
            _resourceManager = resourceManager;
            _selection = selection;
            _config = config;

            _config.Changed += ConfigChanged;
            BarrackSystem.Changed += EnergyChanged;

            SpawnPoint = spawnPoint.Value + Vector3.up * 2f;
            
            Timers = new Timer[RecruitGroup.squadMaxSize];
            for (int i = 0; i < Timers.Length; i++)
            {
                Timers[i] = new Timer(config.Value.RespawnCooldown);
                Timers[i].IsPaused = true;
            }
        }

        private void Update() => _currentState = _currentState.Update(this);

        void IInputPointerable.OnPointerEnter() => _selection.Value = SelectionType.Pointer;
        void IInputPointerable.OnPointerExit() => _selection.Value = SelectionType.None;
        void IInputSelectable.OnSelected() => _selection.Value = SelectionType.Select;
        void IInputSelectable.OnDeselected() { _selection.Value = SelectionType.None; _recruitsPointer.IfNotNull(x => x.DestroyObject()); }

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

        IInputState IInputSelectable.OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (_recruitsPointer == null)
                return currentState;

            SpawnPoint = _recruitsPointer.transform.position;
            return new InputIdleState();
        }

        IInputState IInputSelectable.OnSelectedUpdate(IInputState currentState, Ray ray)
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

        private void EnergyChanged(IEnergySystem _) => ConfigChanged(_config);

        private void ConfigChanged(BarrackBuildingConfig barrackConfig)
        {
            if (barrackConfig.NextLevelConfig == null &&
                BarrackSystem.CurrentEnergy == BarrackSystem.MaxEnergy)
            {
                BarrackSystem.Settings.PassiveSkill.EnableSkill(gameObject);
            }
            else
            {
                BarrackSystem.Settings.PassiveSkill.DisableSkill(gameObject);
            }
        }
    }
}