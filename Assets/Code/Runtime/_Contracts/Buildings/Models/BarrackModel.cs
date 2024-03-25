using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackModel
    {
        public Observable<BarrackBuildingConfig> Config { get; }
        public Observable<bool> IsUnitsPointState { get; } = new();

        public BarrackModel(BarrackBuildingConfig startConfig)
        {
            Config = new Observable<BarrackBuildingConfig>(startConfig);
        }

        public void IncreaseLevel()
        {
            if (Config.Value.NextLevelConfig == null) return;
            Config.Value = Config.Value.NextLevelConfig;
        }

        public void ToggleUnitsPointState() => IsUnitsPointState.Value = !IsUnitsPointState.Value;
    }
}