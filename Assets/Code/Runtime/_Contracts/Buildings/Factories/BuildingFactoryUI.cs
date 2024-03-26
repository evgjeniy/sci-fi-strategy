using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuildingFactoryUI
    {
        public BuildingSelectorMenu CreateSelector(IPlaceholder placeholder);
        public TMenu Create<TMenu>(IBuilding building, string configName = null)
            where TMenu : BuildingManagementMenu;
    }
    
    public class BuildingUIFactory : IBuildingFactoryUI
    {
        private readonly IInstantiator _instantiator;

        public BuildingUIFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public BuildingSelectorMenu CreateSelector(IPlaceholder placeholder)
        {
            var buildingSelector = _instantiator.InstantiatePrefabResourceForComponent<BuildingSelectorMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/UI/{nameof(BuildingSelectorMenu)}",
                extraArgs: new[] { placeholder }
            );
            
            return buildingSelector;
        }

        public TMenu Create<TMenu>(IBuilding building, string configName = null)
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