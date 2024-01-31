using System;
using SustainTheStrain.Buildings.Data;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public interface IBuildingSelector
    {
        public BuildingData SelectedData { get; set; }
        public void CreateBuilding();
    }

    public class BuildingSelector : MonoBehaviour, IBuildingSelector
    {
        private BuildingData _selectedData;

        public BuildingData SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnBuildingDataChanged?.Invoke(_selectedData);
            }
        }

        public event Action<BuildingData> OnCreate;
        public event Action<BuildingData> OnBuildingDataChanged;

        public void CreateBuilding() => OnCreate?.Invoke(_selectedData);

        public void ShowSelectionMenu(BuildingPlaceholder placeholder)
        {
            transform.position = placeholder.SelectorUIRoot.position;
            gameObject.Activate();
        }

        public void HideSelectionMenu(BuildingPlaceholder placeholder)
        {
            gameObject.Deactivate();
        }
    }
}