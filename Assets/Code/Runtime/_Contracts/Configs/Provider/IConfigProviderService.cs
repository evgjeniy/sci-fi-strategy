using SustainTheStrain._Contracts.Configs.Abilities;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Configs
{
    public interface IConfigProviderService
    {
        public TConfig GetAbilityConfig<TConfig>(string configName = null,
            string rootPath = Const.ResourcePath.Abilities.Configs.Root) where TConfig : AbilityConfig;
        public TConfig GetBuildingConfig<TConfig>(long upgrade, string configName = null,
            string rootPath = Const.ResourcePath.Buildings.Configs.Root) where TConfig : BuildingConfig;
        public EnergySystemConfig GetEnergySystemConfig(EnergySystemType type, string configName = null,
            string rootPath = Const.ResourcePath.EnergySystems.Configs.Root);
        public UnitConfig GetUnitConfig(UnitType type, string configName = null,
            string rootPath = Const.ResourcePath.EnergySystems.Configs.Root);
    }
}