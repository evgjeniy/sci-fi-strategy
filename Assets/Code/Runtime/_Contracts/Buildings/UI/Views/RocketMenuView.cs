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
            _rocket.Model.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_rocket.Upgrade);
            _destroyButton.onClick.AddListener(_rocket.Destroy);
        }

        private void OnDisable()
        {
            _rocket.Model.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_rocket.Upgrade);
            _destroyButton.onClick.RemoveListener(_rocket.Destroy);
        }

        private void Display(RocketModel rocketModel)
        {
            if (rocketModel.Config.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold.Value >= rocketModel.Config.NextLevelPrice;
                _upgradePriceText.text = $"{rocketModel.Config.NextLevelPrice}";
            }
            
            _compensationText.text = $"{rocketModel.Config.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_rocket.Model);
    }
}