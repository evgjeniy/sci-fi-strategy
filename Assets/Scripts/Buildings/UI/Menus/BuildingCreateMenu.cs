using System;
using SustainTheStrain.Buildings.Data;

namespace SustainTheStrain.Buildings.UI.Menus
{
    public interface IBuildingCreateMenu
    {
        public BuildingData SelectedData { get; set; }
        public void CreateBuilding();
        
        /* TODO: ----------- Split interface -------------- */
        
        public void Show(BuildingPlaceholder placeholder);
        public void Hide(BuildingPlaceholder placeholder);
        
        public event Action<BuildingData> OnCreateRequested;
        public event Action<BuildingData> OnBuildingDataChanged;
    }
    
    public class BuildingCreateMenu : BuildingMenu, IBuildingCreateMenu
    {
        private BuildingData _selectedData;

        public event Action<BuildingData> OnCreateRequested;
        public event Action<BuildingData> OnBuildingDataChanged;

        public BuildingData SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnBuildingDataChanged?.Invoke(_selectedData);
            }
        }

        public void CreateBuilding() => OnCreateRequested?.Invoke(_selectedData);
    }
}