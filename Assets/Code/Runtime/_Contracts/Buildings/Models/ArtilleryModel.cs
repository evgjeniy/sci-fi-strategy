using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryModel : IObservable<ArtilleryModel>
    {
        private event System.Action<ArtilleryModel> OnChangedEvent = _ => { };

        public event System.Action<ArtilleryModel> Changed
        {
            add
            {
                OnChangedEvent += value;
                value(this);
            }
            remove => OnChangedEvent -= value;
        }

        public ArtilleryBuildingConfig Config { get; private set; }

        public ArtilleryModel(ArtilleryBuildingConfig startConfig) => Config = startConfig;

        public void IncreaseLevel()
        {
            if (Config.NextLevelConfig == null) return;
            Config = Config.NextLevelConfig;

            OnChangedEvent(this);
        }
    }
}