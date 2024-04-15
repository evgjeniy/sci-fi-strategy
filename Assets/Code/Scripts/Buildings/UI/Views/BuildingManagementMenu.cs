using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.ResourceSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public abstract class BuildingManagementMenu<TConfig> : MonoBehaviour where TConfig : BuildingConfig
    {
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _destroyButton;

        [Inject] private IResourceManager _resourceManager;
        [Inject] private IObservable<SelectionType> _selection;
        [Inject] private IObservable<TConfig> _config;

        private int _currentGold;
        private int _nextLevelPrice;
        private int _buildingDestroyCompensation;
        
        protected abstract IBuilding Building { get; }

        protected virtual void Awake()
        {
            Building.ConfigChanged += OnConfigChanged;
            _selection.Changed += OnSelectionChanged;
            _resourceManager.Gold.Changed += OnGoldChanged;

            _upgradeButton.onClick.AddListener(Building.Upgrade);
            _destroyButton.onClick.AddListener(Building.Destroy);
        }

        private void OnConfigChanged(BuildingConfig buildingConfig)
        {
            _nextLevelPrice = buildingConfig.NextLevelPrice;
            _buildingDestroyCompensation = buildingConfig.Compensation;
            
            Display();
        }

        protected virtual void OnDestroy()
        {
            Building.ConfigChanged -= OnConfigChanged;
            _selection.Changed -= OnSelectionChanged;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(Building.Upgrade);
            _destroyButton.onClick.RemoveListener(Building.Destroy);
        }
        
        protected virtual void OnGoldChanged(int currentGold)
        {
            _currentGold = currentGold;

            Display();
        }

        protected virtual void OnConfigChanged(TConfig buildingConfig)
        {
            _nextLevelPrice = buildingConfig.NextLevelPrice;
            _buildingDestroyCompensation = buildingConfig.Compensation;

            Display();
        }

        protected virtual void OnSelectionChanged(SelectionType selectionType)
        {
            gameObject.SetActive(selectionType == SelectionType.Select);
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