using SustainTheStrain.Buildings.Components.GFX;
using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;

namespace SustainTheStrain.Buildings.Components
{
    public class Barrack : Building
    {
        private BuildingGraphics<BarrackData.Stats> _graphics;
        
        public BarrackData Data { get; private set; }
        public BarrackData.Stats CurrentStats => Data.BarrackStats[CurrentUpgradeLevel].Stats;
        protected override int MaxUpgradeLevel => Data.BarrackStats.Length - 1;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            Data = staticDataService.GetBuilding<BarrackData>();

            _graphics = new BuildingGraphics<BarrackData.Stats>(this, Data.BarrackStats);
            
            CurrentUpgradeLevel = 0;
        }

        private void OnDestroy() => _graphics.Destroy();
    }
}