using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryModel
    {
        public Observable<ArtilleryBuildingConfig> Config { get; }

        public ArtilleryModel(ArtilleryBuildingConfig startConfig)
        {
            Config = new Observable<ArtilleryBuildingConfig>(startConfig);
        }

        public void IncreaseLevel()
        {
            if (Config.Value.NextLevelConfig == null) return;
            Config.Value = Config.Value.NextLevelConfig;
        }
    }
}