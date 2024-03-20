using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class LaserModel : IObservable<LaserModel>
    {
        private LaserBuildingConfig _config;

        public Laser Laser { get; }
        public int Level => _config.Level;
        public int Price => _config.Price;
        public int NextLevelPrice => _config.NextLevelPrice;
        public int Compensation => _config.Compensation;
        public float Radius => _config.Radius;
        public float Damage => _config.Damage;
        public float Cooldown => _config.Cooldown;

        private event System.Action<LaserModel> OnChangedEvent = _ => { };

        public LaserModel(Laser laser, LaserBuildingConfig startConfig)
        {
            Laser = laser;
            _config = startConfig;
        }

        public void IncreaseLevel()
        {
            if (_config.NextLevelConfig == null) return;
            _config = _config.NextLevelConfig;

            OnChangedEvent(this);
        }

        public event System.Action<LaserModel> Changed
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