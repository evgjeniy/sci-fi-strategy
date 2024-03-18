using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Rocket : Building
    {
        private IConfigProviderService _configProvider;
        private RocketBuildingConfig _config;

        [Inject]
        private void Construct(IConfigProviderService configProvider) => _configProvider = configProvider;

        private void Awake() => Upgrade();
        
        [NaughtyAttributes.Button]
        private void Upgrade()
        {
            CurrentLevel++;
            _config = _configProvider.GetBuildingConfig<RocketBuildingConfig>(CurrentLevel);
            Debug.Log(_config.Radius);
        }
    }
}