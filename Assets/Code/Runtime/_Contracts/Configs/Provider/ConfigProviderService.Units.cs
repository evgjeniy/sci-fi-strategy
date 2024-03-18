namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public UnitConfig GetUnitConfig(UnitType type, string configName = null,
            string rootPath = Const.ResourcePath.EnergySystems.Configs.Root)
        {
            _unitConfigs ??= Load.Configs<UnitType, UnitConfig>(rootPath, c => c.Type);
            if (_unitConfigs.TryGetValue(type, out var config)) return config;

            configName ??= type + "Config";
            if (!type.TryLoadTypedConfig(rootPath, configName, out UnitConfig loadedConfig))
                throw new System.IO.FileNotFoundException($"File {configName} not founded by path: {rootPath}");

            _unitConfigs.Add(loadedConfig.Type, loadedConfig);
            return loadedConfig;
        }
    }
}