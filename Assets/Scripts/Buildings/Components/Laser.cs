using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;

namespace SustainTheStrain.Buildings.Components
{
    public class Laser : Building
    {
        private PricedLevelStats<LaserData.Stats>[] _stats;
        
        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _stats = staticDataService.GetBuilding<LaserData>().LaserStats;
            CurrentUpgradeLevel = 0;
        }
    }
}