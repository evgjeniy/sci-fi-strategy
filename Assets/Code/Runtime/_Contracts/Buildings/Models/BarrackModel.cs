using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackModel : IObservable<BarrackModel>
    {
        private BarrackBuildingConfig _config;

        public Barrack Barrack { get; }
        public int Level => _config.Level;
        public int Price => _config.Price;
        public int NextLevelPrice => _config.NextLevelPrice;
        public int Compensation => _config.Compensation;
        public float Radius => _config.Radius;
        public float UnitMaxHealth => _config.UnitMaxHealth;
        public float UnitAttackDamage => _config.UnitAttackDamage;
        public float UnitAttackCooldown => _config.UnitAttackCooldown;
        public float RespawnCooldown => _config.RespawnCooldown;

        private event System.Action<BarrackModel> OnChangedEvent = _ => { };

        public BarrackModel(Barrack barrack, BarrackBuildingConfig startConfig)
        {
            Barrack = barrack;
            _config = startConfig;
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