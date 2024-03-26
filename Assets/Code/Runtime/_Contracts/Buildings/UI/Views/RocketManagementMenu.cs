using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketManagementMenu : BuildingManagementMenu
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;

        private Rocket _rocket;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(Rocket rocket, IResourceManager resourceManager)
        {
            _rocket = rocket;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _rocket.Data.Config.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_rocket.Upgrade);
            _destroyButton.onClick.AddListener(_rocket.Destroy);
        }

        private void OnDisable()
        {
            _rocket.Data.Config.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_rocket.Upgrade);
            _destroyButton.onClick.RemoveListener(_rocket.Destroy);
        }

        private void Display(RocketBuildingConfig rocketConfig)
        {
            if (rocketConfig.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold >= rocketConfig.NextLevelPrice;
                _upgradePriceText.text = $"{rocketConfig.NextLevelPrice}";
            }
            
            _compensationText.text = $"{rocketConfig.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_rocket.Data.Config);
    }
}