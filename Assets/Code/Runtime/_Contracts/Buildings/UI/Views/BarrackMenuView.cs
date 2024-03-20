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

        private BarrackModel _barrackModel;
        private IResourceManager _resourceManager;

        [Inject]
        private void Construct(BarrackModel barrackModel, IResourceManager resourceManager)
        {
            _barrackModel = barrackModel;
            _resourceManager = resourceManager;
        }

        private void OnEnable()
        {
            _barrackModel.Changed += Display;
            _resourceManager.Gold.Changed += OnGoldChanged;
            
            _upgradeButton.onClick.AddListener(_barrackModel.Barrack.Upgrade);
            _destroyButton.onClick.AddListener(_barrackModel.Barrack.Destroy);
            _unitsPointButton.onClick.AddListener(() => Debug.Log("SET UNITS POINT"));
        }

        private void OnDisable()
        {
            _barrackModel.Changed -= Display;
            _resourceManager.Gold.Changed -= OnGoldChanged;
            
            _upgradeButton.onClick.RemoveListener(_barrackModel.Barrack.Upgrade);
            _destroyButton.onClick.RemoveListener(_barrackModel.Barrack.Destroy);
        }

        private void Display(BarrackModel barrackModel)
        {
            if (barrackModel.NextLevelPrice == int.MaxValue)
            {
                _upgradeButton.interactable = false;
                _upgradePriceText.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = _resourceManager.Gold.Value >= barrackModel.NextLevelPrice;
                _upgradePriceText.text = $"{barrackModel.NextLevelPrice}";
            }
            
            _compensationText.text = $"{barrackModel.Compensation}";
        }

        private void OnGoldChanged(int currentGold) => Display(_barrackModel);
    }
}