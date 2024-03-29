using UnityEngine;

namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public UnitConfig GetUnitConfig(UnitType type, string configName = null,
            string rootPath = Const.ResourcePath.Units.Configs.Root)
        {
            _unitConfigs ??= Load.Configs<UnitType, UnitConfig>(rootPath, c => c.Type);
            if (_unitConfigs.TryGetValue(type, out var config)) return config;

            configName ??= type + "Config";
            var loadedConfig = Resources.Load<UnitConfig>($"{rootPath}/{configName}");
            if (loadedConfig == null)
                return null;

            _unitConfigs.Add(loadedConfig.Type, loadedConfig);
            return loadedConfig;
        }
    }
}