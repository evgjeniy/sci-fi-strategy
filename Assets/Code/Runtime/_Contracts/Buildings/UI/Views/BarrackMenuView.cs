using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackMenuView : BuildingMenuView
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _unitsPointButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private TMP_Text _upgradePriceText;
        [SerializeField] private TMP_Text _compensationText;

        private Barrack _barrack;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(Barrack barrack, IResourceManager resourceManager)
        {
            _barrack = barrack;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _barrack.Model.Config.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_barrack.Upgrade);
            _destroyButton.onClick.AddListener(_barrack.Destroy);
            _unitsPointButton.onClick.AddListener(_barrack.UnitsPointStateToggle);
        }

        private void OnDisable()
        {
            _barrack.Model.Config.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_barrack.Upgrade);
            _destroyButton.onClick.RemoveListener(_barrack.Destroy);
            _unitsPointButton.onClick.RemoveListener(_barrack.UnitsPointStateToggle);
        }

        private void Display(BarrackBuildingConfig barrackConfig)
        {
            if (barrackConfig.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold >= barrackConfig.NextLevelPrice;
                _upgradePriceText.text = $"{barrackConfig.NextLevelPrice}";
            }
            
            _compensationText.text = $"{barrackConfig.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_barrack.Model.Config);
    }
}