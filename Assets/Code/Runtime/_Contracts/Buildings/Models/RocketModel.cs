using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketModel
    {
        public Observable<RocketBuildingConfig> Config { get; }

        public RocketModel(RocketBuildingConfig startConfig)
        {
            Config = new Observable<RocketBuildingConfig>(startConfig);
        }

        public void IncreaseLevel()
        {
            if (Config.Value.NextLevelConfig == null) return;
            Config.Value = Config.Value.NextLevelConfig;
        }
    }
}