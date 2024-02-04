using SustainTheStrain.Buildings.Data;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings.Components
{
    public abstract class Building : MonoBehaviour
    {
        [Inject] public BuildingSystem BuildingSystem { get; set; }

        private int _currentUpgradeLevel = -1;

        public event System.Action<int> OnLevelUpgrade;

        protected abstract int MaxUpgradeLevel { get; }
        public abstract int UpgradePrice { get; }
        public abstract int DestroyCompensation { get; }

        public int CurrentUpgradeLevel
        {
            get => _currentUpgradeLevel;
            set
            {
                if (value > MaxUpgradeLevel || value < 0 || _currentUpgradeLevel == value) return;
                OnLevelUpgrade?.Invoke(_currentUpgradeLevel = value);
            }
        }

        public class Factory : PlaceholderFactory<BuildingData, Building> {}
    }
    
    public class BuildingFactory : IFactory<BuildingData, Building>
    {
        private readonly DiContainer _diContainer;
        public BuildingFactory(DiContainer diContainer) => _diContainer = diContainer;
        public Building Create(BuildingData param) => _diContainer.InstantiatePrefabForComponent<Building>(param.Prefab);
    }
}