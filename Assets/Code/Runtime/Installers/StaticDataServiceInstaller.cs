using System;
using System.Collections.Generic;
using System.Linq;
using SustainTheStrain.AbilitiesNew;
using SustainTheStrain.Scriptable.Buildings;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Installers
{
    public class StaticDataServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        }
    }

    public interface IStaticDataService
    {
        public T GetBuilding<T>() where T : BuildingData, new();
        public T GetAbilityData<T>() where T : AbilityData, new();
    }

    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<Type, BuildingData> _buildingData = LoadAllBuildings();
        private readonly Dictionary<Type, AbilityData> _abilitiesData = LoadAllAbilities();

        public T GetBuilding<T>() where T : BuildingData, new()
        {
            return _buildingData.TryGetValue(typeof(T), out var buildingData) ? buildingData as T : null;
        }

        public T GetAbilityData<T>() where T : AbilityData, new()
        {
            return _abilitiesData.TryGetValue(typeof(T), out var abilityData) ? abilityData as T : null;
        }

        private static Dictionary<Type, BuildingData> LoadAllBuildings()
        {
            var buildingsData = Resources.LoadAll<BuildingData>("BuildingData");
            return buildingsData == null
                ? new Dictionary<Type, BuildingData>()
                : buildingsData.ToDictionary(data => data.GetType(), data => data);
        }

        private static Dictionary<Type, AbilityData> LoadAllAbilities()
        {
            var buildingsData = Resources.LoadAll<AbilityData>("AbilityData");
            return buildingsData == null
                ? new Dictionary<Type, AbilityData>()
                : buildingsData.ToDictionary(data => data.GetType(), data => data);
        }
    }
}