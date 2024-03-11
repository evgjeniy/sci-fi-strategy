using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SustainTheStrain.Global
{
    public enum EnergySystemType { Building, Ability, Mine, Hero }
    
    public abstract class BuildingData : ScriptableObject {}
    public abstract class AbilityData : ScriptableObject {}

    public abstract class EnergySystemData : ScriptableObject
    {
        [field: SerializeField] public EnergySystemType Type { get; private set; }
    }
    
    public interface IStaticDataService
    {
        public EnergySystemData GetEnergySystemData(EnergySystemType type);
        public TData GetAbilityData<TData>() where TData : AbilityData;
        public TData GetBuildingData<TData>() where TData : BuildingData;
    }

    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnergySystemType, EnergySystemData> _energySystemDatas = LoadEnergySystemData();
        private Dictionary<Type, AbilityData> _abilityDatas;
        private Dictionary<Type, BuildingData> _staticDatas;

        public EnergySystemData GetEnergySystemData(EnergySystemType type) => 
            _energySystemDatas.GetValueOrDefault(type);

        public TData GetAbilityData<TData>() where TData : AbilityData =>
            _abilityDatas.GetValueOrDefault(typeof(TData)) as TData;

        public TData GetBuildingData<TData>() where TData : BuildingData =>
            _staticDatas.GetValueOrDefault(typeof(TData)) as TData;

        private static Dictionary<EnergySystemType, EnergySystemData> LoadEnergySystemData()
        {
            var energySystemDatas = Resources.LoadAll<EnergySystemData>("");
            return energySystemDatas.ToDictionary(x => x.Type, x => x);
        }
    }
}