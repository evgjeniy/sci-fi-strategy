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

        private ArtilleryModel _artilleryModel;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(ArtilleryModel artilleryModel, IResourceManager resourceManager)
        {
            _artilleryModel = artilleryModel;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _artilleryModel.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_artilleryModel.Artillery.Upgrade);
            _destroyButton.onClick.AddListener(_artilleryModel.Artillery.Destroy);
        }

        private void OnDisable()
        {
            _artilleryModel.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_artilleryModel.Artillery.Upgrade);
            _destroyButton.onClick.RemoveListener(_artilleryModel.Artillery.Destroy);
        }

        private void Display(ArtilleryModel laserModel)
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

        private void OnGoldChanged(int currentGold) => Display(_artilleryModel);
    }
}