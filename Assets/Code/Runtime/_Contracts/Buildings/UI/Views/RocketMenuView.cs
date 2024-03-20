using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketMenuView : BuildingMenuView
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;

        private RocketModel _rocketModel;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(RocketModel rocketModel, IResourceManager resourceManager)
        {
            _rocketModel = rocketModel;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _rocketModel.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_rocketModel.Rocket.Upgrade);
            _destroyButton.onClick.AddListener(_rocketModel.Rocket.Destroy);
        }

        private void OnDisable()
        {
            _rocketModel.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_rocketModel.Rocket.Upgrade);
            _destroyButton.onClick.RemoveListener(_rocketModel.Rocket.Destroy);
        }

        private void Display(RocketModel rocketModel)
        {
            if (rocketModel.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold.Value >= rocketModel.NextLevelPrice;
                _upgradePriceText.text = $"{rocketModel.NextLevelPrice}";
            }
            
            _compensationText.text = $"{rocketModel.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_rocketModel);
    }
}