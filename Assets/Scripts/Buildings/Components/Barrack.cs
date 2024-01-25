using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;

namespace SustainTheStrain.Buildings.Components
{
    public class Barrack : Building
    {
        private PricedLevelStats<BarrackData.Stats>[] _stats;
        
        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _stats = staticDataService.GetBuilding<BarrackData>().BarrackStats;
            CurrentUpgradeLevel = 0;
        }
    }
}