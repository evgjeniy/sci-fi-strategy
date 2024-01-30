using SustainTheStrain.Buildings.Components.GFX;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Buildings.FSM.RocketStates;
using SustainTheStrain.Installers;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Rocket : Building
    {
        private RocketStateMachine _stateMachine;
        private BuildingGraphics<RocketData.Stats> _graphics;

        public RocketData Data { get; private set; }
        public RocketData.Stats CurrentStats => Data.RocketStats[CurrentUpgradeLevel].Stats;
        protected override int MaxUpgradeLevel => Data.RocketStats.Length - 1;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<RocketData>();

            _graphics = new BuildingGraphics<RocketData.Stats>(this, Data.RocketStats);
            _stateMachine = new RocketStateMachine(this);
            
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