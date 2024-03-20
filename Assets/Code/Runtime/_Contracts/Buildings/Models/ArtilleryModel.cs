using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryModel : IObservable<ArtilleryModel>
    {
        private ArtilleryBuildingConfig _config;

        public Artillery Artillery { get; }
        public int Level => _config.Level;
        public int Price => _config.Price;
        public int NextLevelPrice => _config.NextLevelConfig == null ? int.MaxValue : _config.NextLevelConfig.Price;
        public int Compensation => _config.Compensation;
        public float Radius => _config.Radius;
        public float Damage => _config.Damage;
        public float Cooldown => _config.Cooldown;
        public float ExplosionRadius => _config.ExplosionRadius;

        private event System.Action<ArtilleryModel> OnChangedEvent = _ => { };

        public ArtilleryModel(Artillery artillery, IConfigProviderService configProvider, int upgradeLevel = 1)
        {
            Artillery = artillery;
            _config = configProvider.GetBuildingConfig<ArtilleryBuildingConfig>(upgradeLevel);
        }

        public void IncreaseLevel()
        {
            if (_config.NextLevelConfig == null) return;
            _config = _config.NextLevelConfig;

            OnChangedEvent(this);
        }

        public event System.Action<ArtilleryModel> Changed
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