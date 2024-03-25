using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class LaserModel
    {
        public Observable<LaserBuildingConfig> Config { get; }

        public LaserModel(LaserBuildingConfig startConfig)
        {
            Config = new Observable<LaserBuildingConfig>(startConfig);
        }

        public void IncreaseLevel()
        {
            if (Config.Value.NextLevelConfig == null) return;
            Config.Value = Config.Value.NextLevelConfig;
        }
    }
}