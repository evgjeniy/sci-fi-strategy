using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryMenuView : BuildingMenuView
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
            _artillery.Model.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_artillery.Upgrade);
            _destroyButton.onClick.AddListener(_artillery.Destroy);
        }

        private void OnDisable()
        {
            _artillery.Model.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_artillery.Upgrade);
            _destroyButton.onClick.RemoveListener(_artillery.Destroy);
        }

        private void Display(ArtilleryModel laserModel)
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

        private void OnGoldChanged(int currentGold) => Display(_artillery.Model);
    }
}