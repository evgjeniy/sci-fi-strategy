using NaughtyAttributes;
using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public abstract class Building : MonoBehaviour
    {
        private int _currentUpgradeLevel = -1;

        public event System.Action<int> OnLevelUpgrade;

        protected abstract int MaxUpgradeLevel { get; }

        public int CurrentUpgradeLevel
        {
            get => _currentUpgradeLevel;
            set
            {
                if (value > MaxUpgradeLevel || value < 0 || _currentUpgradeLevel == value)
                    return;
                
                _currentUpgradeLevel = value;
                OnLevelUpgrade?.Invoke(_currentUpgradeLevel);
            }
        }

        [Button] private void UpgradeLevel() => CurrentUpgradeLevel++;
        [Button] private void DegradeLevel() => CurrentUpgradeLevel--;

        public class Factory : Zenject.PlaceholderFactory<Building> {}
    }
}