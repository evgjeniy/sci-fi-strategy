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
            _laser.Model.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_laser.Upgrade);
            _destroyButton.onClick.AddListener(_laser.Destroy);
        }

        private void OnDisable()
        {
            _laser.Model.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_laser.Upgrade);
            _destroyButton.onClick.RemoveListener(_laser.Destroy);
        }

        private void Display(LaserModel laserModel)
        {
            if (laserModel.Config.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold.Value >= laserModel.Config.NextLevelPrice;
                _upgradePriceText.text = $"{laserModel.Config.NextLevelPrice}";
            }
            
            _compensationText.text = $"{laserModel.Config.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_laser.Model);
    }
}