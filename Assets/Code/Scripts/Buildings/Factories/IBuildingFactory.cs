using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IBuildingFactory
    {
        public TBuilding CreateBuilding<TBuilding>(IPlaceholder placeholder) where TBuilding : IBuilding;
        public TGraphics CreateGfx<TGraphics>(TGraphics prefab, Transform parent, params object[] extraArgs) where TGraphics : Object;
        public BuildingSelectorMenu CreateSelector(IPlaceholder placeholder);
        public TMenu CreateMenu<TMenu>(IBuilding building, string configName = null)
            where TMenu : BuildingManagementMenu;
    }
}