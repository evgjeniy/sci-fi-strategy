using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryManagementMenu : BuildingManagementMenu
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;

        private Artillery _artillery;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(Artillery artillery, IResourceManager resourceManager)
        {
            _artillery = artillery;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _artillery.Data.Config.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_artillery.Upgrade);
            _destroyButton.onClick.AddListener(_artillery.Destroy);
        }

        private void OnDisable()
        {
            _artillery.Data.Config.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_artillery.Upgrade);
            _destroyButton.onClick.RemoveListener(_artillery.Destroy);
        }

        private void Display(ArtilleryBuildingConfig artilleryConfig)
        {
            if (artilleryConfig.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold >= artilleryConfig.NextLevelPrice;
                _upgradePriceText.text = $"{artilleryConfig.NextLevelPrice}";
            }
            
            _compensationText.text = $"{artilleryConfig.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_artillery.Data.Config);
    }
}