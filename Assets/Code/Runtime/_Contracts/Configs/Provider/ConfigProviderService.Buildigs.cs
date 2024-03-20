using SustainTheStrain._Contracts.Configs.Buildings;
using UnityEngine;

namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public TConfig GetBuildingConfig<TConfig>(long upgrade, string configName, string rootPath) where TConfig : BuildingConfig
        {
            _buildingConfigs ??= Load.Configs<string, BuildingConfig>(rootPath, c => c.GetType().Name + c.Level);
            if (_buildingConfigs.TryGetValue(typeof(TConfig).Name + upgrade, out var config)) return config as TConfig;

            configName ??= typeof(TConfig).Name + upgrade;
            var loadedConfig = Resources.Load<TConfig>($"{rootPath}/{configName}");
            if (loadedConfig == null)
            {
#if !UNITY_EDITOR
                throw new System.IO.FileNotFoundException($"File {configName} not founded by path: {rootPath}");
#endif
                return null;
            }

            _buildingConfigs.TryAdd(typeof(TConfig).Name + loadedConfig.Level, loadedConfig);
            return loadedConfig;
        }
    }
}