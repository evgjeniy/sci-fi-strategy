using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class LaserMenuView : BuildingMenuView
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;

        private LaserModel _laserModel;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(LaserModel laserModel, IResourceManager resourceManager)
        {
            _laserModel = laserModel;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _laserModel.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_laserModel.Laser.Upgrade);
            _destroyButton.onClick.AddListener(_laserModel.Laser.Destroy);
        }

        private void OnDisable()
        {
            _laserModel.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_laserModel.Laser.Upgrade);
            _destroyButton.onClick.RemoveListener(_laserModel.Laser.Destroy);
        }

        private void Display(LaserModel laserModel)
        {
            if (laserModel.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold.Value >= laserModel.NextLevelPrice;
                _upgradePriceText.text = $"{laserModel.NextLevelPrice}";
            }
            
            _compensationText.text = $"{laserModel.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_laserModel);
    }
}