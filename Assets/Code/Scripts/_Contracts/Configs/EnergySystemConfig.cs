using UnityEngine;

namespace SustainTheStrain._Contracts.Configs
{
    public enum EnergySystemType : byte { BasicSystem, Ability, Building, Generator, Hero, Shield }

    [CreateAssetMenu(fileName = nameof(EnergySystemConfig), menuName = "Configs/" + nameof(EnergySystemConfig), order = Const.Order.EnergyConfigs)]
    public class EnergySystemConfig : TypedConfig<EnergySystemType>
    {
        [field: SerializeField, Min(1)] public int MaxEnergy { get; private set; }
        [field: SerializeField, Min(1)] public int EnergySpend { get; private set; }
    }

    [CreateAssetMenu(fileName = nameof(EnergyControllerConfig), menuName = "Configs/" + nameof(EnergyControllerConfig), order = Const.Order.EnergyConfigs)]
    public class EnergyControllerConfig : ScriptableObject
    {
        [field: SerializeField] public EnergySystemType[] Systems { get; private set; }
        [field: SerializeField] public int MaxEnergy { get; private set; }
        [field: SerializeField] public int SellableEnergy { get; private set; }

        public int AvailableEnergy => MaxEnergy - SellableEnergy;
    }
}