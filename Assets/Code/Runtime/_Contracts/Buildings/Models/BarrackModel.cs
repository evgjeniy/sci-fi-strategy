using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackModel : IObservable<BarrackModel>
    {
        private event System.Action<BarrackModel> OnChangedEvent = _ => { };

        public event System.Action<BarrackModel> Changed
        {
            add
            {
                OnChangedEvent += value;
                value(this);
            }
            remove => OnChangedEvent -= value;
        }

        public BarrackBuildingConfig Config { get; private set; }
        public bool IsUnitsPointState { get; private set; }

        public BarrackModel(BarrackBuildingConfig startConfig) => Config = startConfig;

        public void IncreaseLevel()
        {
            if (Config.NextLevelConfig == null) return;
            Config = Config.NextLevelConfig;

            OnChangedEvent(this);
        }

        public void ToggleUnitsPointState() => IsUnitsPointState = !IsUnitsPointState;
    }
}