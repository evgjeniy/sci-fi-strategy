using System;
using SustainTheStrain._Contracts.Configs.Abilities;
using UnityEngine;

namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService
    {
        public TConfig GetAbilityConfig<TConfig>(string configName = null,
            string rootPath = Const.ResourcePath.Abilities.Configs.Root) where TConfig : AbilityConfig
        {
            _abilityConfigs ??= Load.Configs<Type, AbilityConfig>(rootPath, c => c.GetType());
            if (_abilityConfigs.TryGetValue(typeof(TConfig), out var config)) return config as TConfig;
            
            configName ??= typeof(TConfig).Name;
            var loadedConfig = Resources.Load<TConfig>($"{rootPath}/{configName}");
            if (loadedConfig == null)
                return null;

            _abilityConfigs.TryAdd(typeof(TConfig), loadedConfig);
            return loadedConfig;
        }
    }
}