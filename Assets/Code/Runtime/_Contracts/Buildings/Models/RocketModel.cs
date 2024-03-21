using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketModel : IObservable<RocketModel>
    {
        private event System.Action<RocketModel> OnChangedEvent = _ => { };

        public event System.Action<RocketModel> Changed
        {
            add
            {
                OnChangedEvent += value;
                value(this);
            }
            remove => OnChangedEvent -= value;
        }

        public RocketBuildingConfig Config { get; private set; }

        public RocketModel(RocketBuildingConfig startConfig) => Config = startConfig;

        public void IncreaseLevel()
        {
            if (Config.NextLevelConfig == null) return;
            Config = Config.NextLevelConfig;

            OnChangedEvent(this);
        }
    }
}