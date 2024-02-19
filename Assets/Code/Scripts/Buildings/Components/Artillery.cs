using SustainTheStrain.Buildings.FSM;
using SustainTheStrain.Installers;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Artillery : Building
    {
        private ArtilleryStateMachine _stateMachine;
        private BuildingGraphics<ArtilleryData.Stats> _graphics;

        public ArtilleryData Data { get; private set; }
        public ArtilleryData.Stats CurrentStats => Data.ArtilleryStats[CurrentUpgradeLevel].Stats;
        protected override int MaxUpgradeLevel => Data.ArtilleryStats.Length - 1;
        public override int UpgradePrice => Data.ArtilleryStats[CurrentUpgradeLevel].NextLevelPrice;
        public override int DestroyCompensation => Data.ArtilleryStats[CurrentUpgradeLevel].DestroyCompensation;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<ArtilleryData>();

            _graphics = new BuildingGraphics<ArtilleryData.Stats>(this, Data.ArtilleryStats);
            _stateMachine = new ArtilleryStateMachine(this);

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