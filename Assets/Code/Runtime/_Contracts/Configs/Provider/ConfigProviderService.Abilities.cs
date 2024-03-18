using System;
using SustainTheStrain._Contracts.Configs.Abilities;

namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public TConfig GetAbilityConfig<TConfig>(string configName = nameof(TConfig),
            string rootPath = Const.ResourcePath.Abilities.Configs.Root) where TConfig : AbilityConfig
        {
            _abilityConfigs ??= Load.Configs<Type, AbilityConfig>(rootPath, c => c.GetType());
            if (_abilityConfigs.TryGetValue(typeof(TConfig), out var config)) return config as TConfig;
            
            if (!Load.TryLoadConfig<TConfig>(rootPath, configName, out var loadedConfig))
                throw new System.IO.FileNotFoundException($"File {configName} not founded by path: {rootPath}");

            _abilityConfigs.Add(typeof(TConfig), loadedConfig);
            return loadedConfig;
        }
    }
}