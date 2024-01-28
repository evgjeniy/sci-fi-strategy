using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Buildings.FSM.LaserStates;
using SustainTheStrain.Installers;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Laser : Building
    {
        private LaserStateMachine _stateMachine;

        public LaserData Data { get; private set; }
        public LaserData.Stats CurrentStats => Data.LaserStats[CurrentUpgradeLevel].Stats;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<LaserData>();
            CurrentUpgradeLevel = 0;

            _stateMachine = new LaserStateMachine(this);
        }

        private void Update() => _stateMachine.Run();

        private void OnDrawGizmos()
        {
            if (Data == null) return;

            Gizmos.DrawWireSphere(transform.position, CurrentStats.AttackRadius);
        }
    }
}