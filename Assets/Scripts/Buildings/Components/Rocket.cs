using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;

namespace SustainTheStrain.Buildings.Components
{
    public class Rocket : Building 
    {
        private PricedLevelStats<RocketData.Stats>[] _stats;
        
        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _stats = staticDataService.GetBuilding<RocketData>().RocketStats;
            CurrentUpgradeLevel = 0;
        }
    }
}