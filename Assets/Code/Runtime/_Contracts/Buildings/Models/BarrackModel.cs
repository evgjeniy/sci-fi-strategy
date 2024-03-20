using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackModel : IObservable<BarrackModel>
    {
        private BarrackBuildingConfig _config;

        public Barrack Artillery { get; }
        public int Level => _config.Level;
        public int Price => _config.Price;
        public int NextLevelPrice => _config.NextLevelConfig == null ? int.MaxValue : _config.NextLevelConfig.Price;
        public int Compensation => _config.Compensation;
        public float Radius => _config.Radius;

        private event System.Action<BarrackModel> OnChangedEvent = _ => { };

        public BarrackModel(Barrack artillery, IConfigProviderService configProvider, int upgradeLevel = 1)
        {
            Artillery = artillery;
            _config = configProvider.GetBuildingConfig<BarrackBuildingConfig>(upgradeLevel);
        }

        public void IncreaseLevel()
        {
            if (_config.NextLevelConfig == null) return;
            _config = _config.NextLevelConfig;

            OnChangedEvent(this);
        }

        public event System.Action<BarrackModel> Changed
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