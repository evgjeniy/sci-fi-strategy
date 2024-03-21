using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class LaserModel : IObservable<LaserModel>
    {
        private event System.Action<LaserModel> OnChangedEvent = _ => { };

        public event System.Action<LaserModel> Changed
        {
            add
            {
                OnChangedEvent += value;
                value(this);
            }
            remove => OnChangedEvent -= value;
        }

        public LaserBuildingConfig Config { get; private set; }

        public LaserModel(LaserBuildingConfig startConfig) => Config = startConfig;

        public void IncreaseLevel()
        {
            if (Config.NextLevelConfig == null) return;
            Config = Config.NextLevelConfig;

            OnChangedEvent(this);
        }
    }
}