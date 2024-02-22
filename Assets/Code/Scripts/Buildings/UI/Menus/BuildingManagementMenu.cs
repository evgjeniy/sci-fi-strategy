using System;

namespace SustainTheStrain.Buildings.UI
{
    public interface IBuildingManagementMenu
    {
        public void UpgradeRequest();
        public void DestroyRequest();
        
        /* TODO: ----------- Split interface -------------- */
        
        public void Show(BuildingPlaceholder placeholder);
        public void Hide(BuildingPlaceholder placeholder);
        
        public event Action OnUpgradeRequested;
        public event Action OnDestroyRequested;
    }

    public class BuildingManagementMenu : BuildingMenu, IBuildingManagementMenu
    {
        public event Action OnUpgradeRequested;
        public event Action OnDestroyRequested;
        
        public void UpgradeRequest() => OnUpgradeRequested?.Invoke();
        public void DestroyRequest() => OnDestroyRequested?.Invoke();
    }
}