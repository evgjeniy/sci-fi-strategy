using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.ResourceSystems;
using TMPro;
using UnityEngine;
using UnityEngine.Extensions;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BuildingManagementMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _menuRoot;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _destroyButton;

        [Inject] private IResourceManager _resourceManager;

        private int _currentGold;
        private int _nextLevelPrice;
        private int _buildingDestroyCompensation;

        public void Enable() => _menuRoot.Activate();
        public void Disable() => _menuRoot.IfNotNull(x => x.Deactivate());

        protected void SubscribeBaseEvents(IBuilding building)
        {
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(building.Upgrade);
            _destroyButton.onClick.AddListener(building.Destroy);
        }

        protected void UnsubscribeBaseEvents(IBuilding building)
        {
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(building.Upgrade);
            _destroyButton.onClick.RemoveListener(building.Destroy);
        }
        
        protected virtual void OnGoldChanged(int currentGold)
        {
            _currentGold = currentGold;

            Display();
        }

        protected virtual void OnConfigChanged(BuildingConfig buildingConfig)
        {
            _nextLevelPrice = buildingConfig.NextLevelPrice;
            _buildingDestroyCompensation = buildingConfig.Compensation;

            Display();
        }

        protected virtual void Display()
        {
            if (_nextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _currentGold >= _nextLevelPrice;
                _upgradePriceText.text = $"{_nextLevelPrice}";
            }

            _compensationText.text = $"{_buildingDestroyCompensation}";
        }
    }
}