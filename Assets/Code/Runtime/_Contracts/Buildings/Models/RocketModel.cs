using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketModel : IObservable<RocketModel>
    {
        private RocketBuildingConfig _config;

        public Rocket Rocket { get; }
        public int Level => _config.Level;
        public int Price => _config.Price;
        public int NextLevelPrice => _config.NextLevelConfig == null ? int.MaxValue : _config.NextLevelConfig.Price;
        public int Compensation => _config.Compensation;
        public float Radius => _config.Radius;
        public float Damage => _config.Damage;
        public float Cooldown => _config.Cooldown;
        public int MaxTargets => _config.MaxTargets;
        public float SectorAngle => _config.SectorAngle;

        private event System.Action<RocketModel> OnChangedEvent = _ => { };

        public RocketModel(Rocket rocket, IConfigProviderService configProvider, int upgradeLevel = 1)
        {
            Rocket = rocket;
            _config = configProvider.GetBuildingConfig<RocketBuildingConfig>(upgradeLevel);
        }

        public void IncreaseLevel()
        {
            if (_config.NextLevelConfig == null) return;
            _config = _config.NextLevelConfig;

            OnChangedEvent(this);
        }

        public event System.Action<RocketModel> Changed
        {
            add
            {
                OnChangedEvent += value;
                value(this);
            }
            remove => OnChangedEvent -= value;
        }
    }
}