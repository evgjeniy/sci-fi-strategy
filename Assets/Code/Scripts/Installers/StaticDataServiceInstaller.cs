using System;
using System.Collections.Generic;
using System.Linq;
using SustainTheStrain.Buildings.Data;
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
    
    public class StaticDataService : IStaticDataService
    {
        // private readonly IAssetProvider _assetProvider;
        
        private readonly Dictionary<Type, BuildingData> _buildingData;

        public StaticDataService(/*IAssetProvider assetProvider*/)
        {
            // _assetProvider = assetProvider;
            
            _buildingData = /*assetProvider.*/LoadAllBuildings();
        }

        public T GetBuilding<T>() where T : BuildingData, new()
        {
            return _buildingData.TryGetValue(typeof(T), out var buildingData) ? buildingData as T : null;
        }

        // TODO : Replace all Resource loading into AssetData provider (maybe using Addressables)
        private static Dictionary<Type, BuildingData> LoadAllBuildings()
        {
            var buildingsData = Resources.LoadAll<BuildingData>("BuildingData");
            return buildingsData == null ? new Dictionary<Type, BuildingData>()
                : buildingsData.ToDictionary(data => data.GetType(), data => data);
        }
    }

    public interface IStaticDataService
    {
        public T GetBuilding<T>() where T : BuildingData, new();
    }
}