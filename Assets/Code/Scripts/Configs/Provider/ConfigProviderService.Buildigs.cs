using SustainTheStrain.Configs.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs
{
    public partial class ConfigProviderService
    {
        public TConfig GetBuildingConfig<TConfig>(int upgrade, string configName = null,
            string rootPath = Const.ResourcePath.Buildings.Configs.Root) where TConfig : BuildingConfig
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