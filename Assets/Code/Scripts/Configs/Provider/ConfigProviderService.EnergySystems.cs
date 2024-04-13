using UnityEngine;

namespace SustainTheStrain.Configs
{
    public partial class ConfigProviderService
    {
        public EnergySystemConfig GetEnergySystemConfig(EnergySystemType type, string configName = null,
            string rootPath = Const.ResourcePath.EnergySystems.Configs.Root)
        {
            _energySystemConfigs ??= Load.Configs<EnergySystemType, EnergySystemConfig>(rootPath, c => c.Type);
            if (_energySystemConfigs.TryGetValue(type, out var config)) return config;

            configName ??= type + "Config";
            var loadedConfig = Resources.Load<EnergySystemConfig>($"{rootPath}/{configName}");
            if (loadedConfig == null)
                return null;
                
            _energySystemConfigs.Add(loadedConfig.Type, loadedConfig);
            return loadedConfig;
        }
    }
}