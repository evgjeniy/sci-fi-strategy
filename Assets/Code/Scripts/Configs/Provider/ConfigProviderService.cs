using System.Collections.Generic;
using System.Linq;
using SustainTheStrain.Configs.Abilities;
using SustainTheStrain.Configs.Buildings;
using UnityEngine;

namespace SustainTheStrain.Configs
{
    public partial class ConfigProviderService : IConfigProviderService
    {
        private Dictionary<System.Type, AbilityConfig> _abilityConfigs = Load.Configs<System.Type, AbilityConfig>
        (
            Const.ResourcePath.Abilities.Configs.Root, config => config.GetType()
        );

        private Dictionary<string, BuildingConfig> _buildingConfigs = Load.Configs<string, BuildingConfig>
        (
            Const.ResourcePath.Buildings.Configs.Root, config => config.GetType().Name + config.Level
        );

        private Dictionary<EnergySystemType, EnergySystemConfig> _energySystemConfigs = Load.Configs<EnergySystemType, EnergySystemConfig>
        (
            Const.ResourcePath.EnergySystems.Configs.Root, config => config.Type
        );

        private Dictionary<UnitType, UnitConfig> _unitConfigs = Load.Configs<UnitType, UnitConfig>
        (
            Const.ResourcePath.Units.Configs.Root, config => config.Type
        );
    }

    public static class Load
    {
        public static Dictionary<TType, TConfig> Configs<TType, TConfig>(string rootPath,
            System.Func<TConfig, TType> keySelector) where TConfig : Object
        {
            var abilityConfigs = Resources.LoadAll<TConfig>(rootPath);
            return abilityConfigs == null ? new Dictionary<TType, TConfig>() : abilityConfigs.ToDictionary(keySelector);
        }
    }
}