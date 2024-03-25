using SustainTheStrain._Contracts.Configs.Buildings;
using UnityEngine;

namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public TConfig GetBuildingConfig<TConfig>(byte upgrade, string configName, string rootPath) where TConfig : BuildingConfig
        {
            _buildingConfigs ??= Load.Configs<string, BuildingConfig>(rootPath, c => c.GetType().Name + c.Level);
            
            var typeName = typeof(TConfig).Name;
            if (_buildingConfigs.TryGetValue(typeName + upgrade, out var config))
                return config as TConfig;

            configName ??= typeName + upgrade;
            var loadedConfig = Resources.Load<TConfig>($"{rootPath}/{configName}");
            if (loadedConfig == null)
                return null;

            _buildingConfigs.TryAdd(typeName + loadedConfig.Level, loadedConfig);
            return loadedConfig;
        }
    }
}