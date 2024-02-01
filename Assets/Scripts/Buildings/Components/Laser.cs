using SustainTheStrain.Buildings.Components.GFX;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Buildings.FSM.LaserStates;
using SustainTheStrain.Installers;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Laser : Building
    {
        private LaserStateMachine _stateMachine;
        private BuildingGraphics<LaserData.Stats> _graphics;

        public LaserData Data { get; private set; }
        public LaserData.Stats CurrentStats => Data.LaserStats[CurrentUpgradeLevel].Stats;
        protected override int MaxUpgradeLevel => Data.LaserStats.Length - 1;

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