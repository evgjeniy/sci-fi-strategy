using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class LaserManagementMenu : BuildingManagementMenu
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;

        private Laser _laser;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(Laser laser, IResourceManager resourceManager)
        {
            _laser = laser;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _laser.Data.Config.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_laser.Upgrade);
            _destroyButton.onClick.AddListener(_laser.Destroy);
        }

        private void OnDisable()
        {
            _laser.Data.Config.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_laser.Upgrade);
            _destroyButton.onClick.RemoveListener(_laser.Destroy);
        }

        private void Display(LaserBuildingConfig laserConfig)
        {
            if (laserConfig.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold >= laserConfig.NextLevelPrice;
                _upgradePriceText.text = $"{laserConfig.NextLevelPrice}";
            }
            
            _compensationText.text = $"{laserConfig.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_laser.Data.Config);
    }
}