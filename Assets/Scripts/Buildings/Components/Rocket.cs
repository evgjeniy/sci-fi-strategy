using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Buildings.FSM.RocketStates;
using SustainTheStrain.Installers;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Rocket : Building
    {
        private RocketStateMachine _stateMachine;

        public RocketData Data { get; private set; }
        public RocketData.Stats CurrentStats => Data.RocketStats[CurrentUpgradeLevel].Stats;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<RocketData>();
            CurrentUpgradeLevel = 0;

            _stateMachine = new RocketStateMachine(this);
        }

        private void Update() => _stateMachine.Run();

        private void OnDrawGizmos()
        {
            if (Data == null) return;

            Gizmos.DrawWireSphere(transform.position, CurrentStats.AttackRadius);
        }
    }
}