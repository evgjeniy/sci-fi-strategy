using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;

namespace SustainTheStrain.Buildings.Components
{
    public class Artillery : Building
    {
        private PricedLevelStats<ArtilleryData.Stats>[] _stats;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _stats = staticDataService.GetBuilding<ArtilleryData>().ArtilleryStats;
            CurrentUpgradeLevel = 0;
        }
    }
}