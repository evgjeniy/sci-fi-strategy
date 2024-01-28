using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Buildings.FSM.ArtilleryStates;
using SustainTheStrain.Installers;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public class Artillery : Building
    {
        private ArtilleryStateMachine _stateMachine;

        public ArtilleryData Data { get; private set; }
        public ArtilleryData.Stats CurrentStats => Data.ArtilleryStats[CurrentUpgradeLevel].Stats;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<ArtilleryData>();
            CurrentUpgradeLevel = 0;

            _stateMachine = new ArtilleryStateMachine(this);
        }

        private void Update() => _stateMachine.Run();

        private void OnDrawGizmos()
        {
            if (Data == null) return;

            Gizmos.DrawWireSphere(transform.position, CurrentStats.AttackRadius);
        }
    }
}