using SustainTheStrain.Configs.Abilities;
using SustainTheStrain.Configs.Buildings;

namespace SustainTheStrain.Configs
{
    public interface IConfigProviderService
    {
        public TConfig GetAbilityConfig<TConfig>(string configName = null,
            string rootPath = Const.ResourcePath.Abilities.Configs.Root) where TConfig : AbilityConfig;
        public TConfig GetBuildingConfig<TConfig>(int upgrade = 1, string configName = null,
            string rootPath = Const.ResourcePath.Buildings.Configs.Root) where TConfig : BuildingConfig;
        public EnergySystemConfig GetEnergySystemConfig(EnergySystemType type, string configName = null,
            string rootPath = Const.ResourcePath.EnergySystems.Configs.Root);
        public UnitConfig GetUnitConfig(UnitType type, string configName = null,
            string rootPath = Const.ResourcePath.EnergySystems.Configs.Root);
    }
}