using SustainTheStrain.Buildings.FSM;
using SustainTheStrain.Installers;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Laser : Building
    {
        [field: SerializeField] public LineRenderer Line { get; private set; }

        private LaserStateMachine _stateMachine;
        private BuildingGraphics<LaserData.Stats> _graphics;

        public LaserData Data { get; private set; }
        public LaserData.Stats CurrentStats => Data.LaserStats[CurrentUpgradeLevel].Stats;
        public override Vector3 Orientation { get; set; }
        protected override int MaxUpgradeLevel => Data.LaserStats.Length - 1;
        public override int UpgradePrice => Data.LaserStats[CurrentUpgradeLevel].NextLevelPrice;
        public override int DestroyCompensation => Data.LaserStats[CurrentUpgradeLevel].DestroyCompensation;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<LaserData>();

            _graphics = new BuildingGraphics<LaserData.Stats>(this, Data.LaserStats);
            _stateMachine = new LaserStateMachine(this);
            
            CurrentUpgradeLevel = 0;
        }

        private void Update() => _stateMachine.Run();

        private void OnDestroy() => _graphics.Destroy();

        private void OnDrawGizmos()
        {
            if (Data == null) return;

            Gizmos.DrawWireSphere(transform.position, CurrentStats.AttackRadius);
        }
    }
}