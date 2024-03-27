using System;
using Cysharp.Threading.Tasks;
using SustainTheStrain.Buildings.FSM;
using SustainTheStrain.Installers;
using SustainTheStrain.Scriptable.Buildings;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Spawners;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings.Components
{
    public class Barrack : Building
    {
        [SerializeField] private RecruitGroup _recruitGroup;
        [SerializeField] private RecruitSpawner _recruitSpawner;

        private Vector3 _orientation;
        
        private Timer _timer;
        private BuildingGraphics<BarrackData.Stats> _graphics;

        public BarrackData Data { get; private set; }
        public BarrackData.Stats CurrentStats => Data.BarrackStats[CurrentUpgradeLevel].Stats;
        protected override int MaxUpgradeLevel => Data.BarrackStats.Length - 1;
        public override int UpgradePrice => Data.BarrackStats[CurrentUpgradeLevel].NextLevelPrice;
        public override int DestroyCompensation => Data.BarrackStats[CurrentUpgradeLevel].DestroyCompensation;

        private event Action<Vector3> OnOrientationChanged; 
        
        public override Vector3 Orientation {
            get => _orientation;
            set
            {
                _orientation = value;
                OnOrientationChanged?.Invoke(value);
            }
        }

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<BarrackData>();
            
            _graphics = new BuildingGraphics<BarrackData.Stats>(this, Data.BarrackStats);
            
            CurrentUpgradeLevel = 0;

            _timer = new Timer(CurrentStats.RespawnCooldown);

            OnOrientationChanged += ChangeGroupPosition;
            OnOrientationChanged += ChangeSpawnerPosition;
        }

        private void ChangeSpawnerPosition(Vector3 position)
        {
            _recruitSpawner.SpawnPosition = transform.position + (position - transform.position).normalized * 3;
        }

        private void ChangeGroupPosition(Vector3 position)
        {
            _recruitGroup.GuardPost.Position = position;
        }
        
        private async void RespawnRecruit()
        {
            if (!_timer.IsTimeOver)
                await UniTask.WaitForSeconds(_timer.Time);

            var newRecruit = _recruitSpawner.Spawn(CurrentStats);
            newRecruit.Deactivate();

            while (!_recruitGroup.AddRecruit(newRecruit))
                await UniTask.NextFrame();
            
            newRecruit.Activate();

            _timer.Time = CurrentStats.RespawnCooldown;
        }
        
        private void UpgradeStats(int _)
        {
            foreach (var recruit in _recruitGroup.Recruits)
                recruit.UpdateStats(CurrentStats);
        }
        

        private void OnEnable()
        {
            //_recruitGroup.OnRecruitRemoved += RespawnRecruit;
            OnLevelUpgrade += UpgradeStats;
        }

        private void OnDisable()
        {
            //_recruitGroup.OnRecruitRemoved -= RespawnRecruit;
            OnLevelUpgrade -= UpgradeStats;
        }

        private void Update()
        {
            _timer.Time -= Time.deltaTime;
            
            if (!_timer.IsTimeOver)
                return;
            if (_recruitGroup.Recruits.Count >= _recruitGroup.squadMaxSize)
                return;
                
            var newRecruit = _recruitSpawner.Spawn(CurrentStats);
            //newRecruit.Deactivate();
            
            _recruitGroup.AddRecruit(newRecruit);
            
            //newRecruit.Activate();

            _timer.Time = CurrentStats.RespawnCooldown;
        }

        private void OnDestroy() => _graphics.Destroy();
    }
}