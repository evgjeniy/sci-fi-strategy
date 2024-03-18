using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public TConfig GetBuildingConfig<TConfig>(long upgrade, string configName, string rootPath) where TConfig : BuildingConfig
        {
            _buildingConfigs ??= Load.Configs<string, BuildingConfig>(rootPath, c => c.GetType().Name + c.Level);
            if (_buildingConfigs.TryGetValue(typeof(TConfig).Name + upgrade, out var config)) return config as TConfig;

            configName ??= typeof(TConfig).Name + upgrade;
            if (!Load.TryLoadConfig<TConfig>(rootPath, configName, out var loadedConfig))
                throw new System.IO.FileNotFoundException($"File {configName} not founded by path: {rootPath}");

            _buildingConfigs.Add(typeof(TConfig).Name + loadedConfig.Level, loadedConfig);
            return loadedConfig;
        }
    }
}