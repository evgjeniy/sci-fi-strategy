using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SustainTheStrain._Contracts.Configs.Abilities;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Configs
{
    public partial class ConfigProviderService : IConfigProviderService
    {
        private Dictionary<System.Type, AbilityConfig> _abilityConfigs;
        private Dictionary<string, BuildingConfig> _buildingConfigs;
        private Dictionary<EnergySystemType, EnergySystemConfig> _energySystemConfigs;
        private Dictionary<UnitType, UnitConfig> _unitConfigs;
    }

    public static class Load
    {
        public static Dictionary<TType, TConfig> Configs<TType, TConfig>(string rootPath,
            System.Func<TConfig, TType> keySelector) where TConfig : Object
        {
            var abilityConfigs = Resources.LoadAll<TConfig>(rootPath);
            return abilityConfigs == null ? new Dictionary<TType, TConfig>() : abilityConfigs.ToDictionary(keySelector);
        }
        public static bool TryLoadConfig<TConfig>(string rootPath, string configName, out TConfig config) where TConfig : Object
        {
            config = Resources.Load<TConfig>($"{rootPath}/{configName}");
            if (config != null) return true;

            var loadedConfigs = Resources.LoadAll<TConfig>(rootPath);
            if (loadedConfigs == null || loadedConfigs.Length == 0) return false;

            config = loadedConfigs[0];
            return true;
        }

        public static bool TryLoadTypedConfig<TConfig, TType>(this TType type, string rootPath, string configName, out TConfig config) where TConfig : TypedConfig<TType>
        {
            config = Resources.Load<TConfig>($"{rootPath}/{configName}");
            if (config != null) return true;
            
            var loadedConfigs = Resources.LoadAll<TConfig>(rootPath);
            if (loadedConfigs == null || loadedConfigs.Length == 0)
                return false;

            var loadedTypeConfig = loadedConfigs.FirstOrDefault(c => c.Type.Equals(type));
            if (loadedTypeConfig == null)
                return false;

            config = loadedTypeConfig;
            return true;
        }
    }
}