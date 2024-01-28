using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;

namespace SustainTheStrain.Buildings.Components
{
    public class Barrack : Building
    {
        public BarrackData Data { get; private set; }
        public BarrackData.Stats CurrentStats => Data.BarrackStats[CurrentUpgradeLevel].Stats;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<BarrackData>();
            CurrentUpgradeLevel = 0;
        }
    }
}