namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public EnergySystemConfig GetEnergySystemConfig(EnergySystemType type, string configName = null,
            string rootPath = Const.ResourcePath.EnergySystems.Configs.Root)
        {
            _energySystemConfigs ??= Load.Configs<EnergySystemType, EnergySystemConfig>(rootPath, c => c.Type);
            if (_energySystemConfigs.TryGetValue(type, out var config)) return config;

            configName ??= type + "Config";
            if (!type.TryLoadTypedConfig(rootPath, configName, out EnergySystemConfig loadedConfig))
                throw new System.IO.FileNotFoundException($"File {configName} not founded by path: {rootPath}");
                
            _energySystemConfigs.Add(loadedConfig.Type, loadedConfig);
            return loadedConfig;
        }
    }
}