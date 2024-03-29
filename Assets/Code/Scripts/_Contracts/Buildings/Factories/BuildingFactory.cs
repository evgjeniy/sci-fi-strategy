using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuildingFactory
    {
        public TBuilding CreateBuilding<TBuilding>(IPlaceholder placeholder) where TBuilding : IBuilding;
        public TGraphics CreateGfx<TGraphics>(TGraphics prefab, Transform parent, params object[] extraArgs) where TGraphics : Object;
        public BuildingSelectorMenu CreateSelector(IPlaceholder placeholder);
        public TMenu CreateMenu<TMenu>(IBuilding building, string configName = null)
            where TMenu : BuildingManagementMenu;
    }
    
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IInstantiator _instantiator;

        public BuildingFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public TBuilding CreateBuilding<TBuilding>(IPlaceholder placeholder) where TBuilding : IBuilding
        {
            var building = _instantiator.InstantiatePrefabResourceForComponent<TBuilding>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/{typeof(TBuilding).Name}",
                extraArgs: new[] { placeholder }
            );
            
            placeholder.SetBuilding(building);
            return building;
        }

        public TGraphics CreateGfx<TGraphics>(TGraphics prefab, Transform parent, params object[] extraArgs) where TGraphics : Object
        {
            return _instantiator.InstantiatePrefabForComponent<TGraphics>(prefab, parent, extraArgs);
        }

        public BuildingSelectorMenu CreateSelector(IPlaceholder placeholder)
        {
            var buildingSelector = _instantiator.InstantiatePrefabResourceForComponent<BuildingSelectorMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/UI/{nameof(BuildingSelectorMenu)}",
                extraArgs: new[] { placeholder }
            );
            
            return buildingSelector;
        }

        public TMenu CreateMenu<TMenu>(IBuilding building, string configName = null)
            where TMenu : BuildingManagementMenu
        {
            var buildingManagementMenu = _instantiator.InstantiatePrefabResourceForComponent<TMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/UI/{configName ?? typeof(TMenu).Name}",
                extraArgs: new[] { building }
            );
            
            buildingManagementMenu.SetParent(building.transform);
            buildingManagementMenu.transform.LookAtCamera(building.transform);
            
            return buildingManagementMenu;
        }
    }
}