using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BuildingSelectorMenu : MonoBehaviour
    {
        [field: SerializeField] private Button _createRocket;
        [field: SerializeField] private Button _createLaser;
        [field: SerializeField] private Button _createArtillery;
        [field: SerializeField] private Button _createBarrack;
        
        [field: SerializeField] private TMP_Text _rocketPriceText;
        [field: SerializeField] private TMP_Text _laserPriceText;
        [field: SerializeField] private TMP_Text _artilleryPriceText;
        [field: SerializeField] private TMP_Text _barrackPriceText;

        private IResourceManager _resourceManager;

        private int _rocketPrice;
        private int _laserPrice;
        private int _artilleryPrice;
        private int _barrackPrice;

        [Inject]
        private void Construct(IPlaceholder placeholder,
            IBuildingFactory buildingFactory,
            IConfigProviderService configProvider,
            IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            
            DisplayCreateBuildingsPrice(configProvider);
            RegisterCreateButtons(placeholder, buildingFactory);

            transform.SetParent(placeholder.transform);
            transform.LookAtCamera(placeholder.transform);
        }

        private void OnEnable() => _resourceManager.Gold.Changed += DisplayButtonInteraction;
        private void OnDisable() => _resourceManager.Gold.Changed -= DisplayButtonInteraction;

        private void DisplayCreateBuildingsPrice(IConfigProviderService configProvider)
        {
            _rocketPrice = configProvider.GetBuildingConfig<RocketBuildingConfig>().Price;
            _laserPrice = configProvider.GetBuildingConfig<LaserBuildingConfig>().Price;
            _artilleryPrice = configProvider.GetBuildingConfig<ArtilleryBuildingConfig>().Price;
            _barrackPrice = configProvider.GetBuildingConfig<BarrackBuildingConfig>().Price;
            
            _rocketPriceText.text = $"{_rocketPrice}";
            _laserPriceText.text = $"{_laserPrice}";
            _artilleryPriceText.text = $"{_artilleryPrice}";
            _barrackPriceText.text = $"{_barrackPrice}";
        }

        private void RegisterCreateButtons(IPlaceholder placeholder, IBuildingFactory buildingFactory)
        {
            _createRocket.onClick.AddListener(() => OnCreateClick<Rocket>(_rocketPrice));
            _createLaser.onClick.AddListener(() => OnCreateClick<Laser>(_laserPrice));
            _createArtillery.onClick.AddListener(() => OnCreateClick<Artillery>(_artilleryPrice));
            _createBarrack.onClick.AddListener(() => OnCreateClick<Barrack>(_barrackPrice));
            return;

            void OnCreateClick<TBuilding>(int price) where TBuilding : IBuilding
            {
                buildingFactory.Create<TBuilding>(placeholder);
                _resourceManager.Gold.Value -= price;
            }
        }

        private void DisplayButtonInteraction(int gold)
        {
            _createRocket.interactable = gold >= _rocketPrice;
            _createLaser.interactable = gold >= _laserPrice;
            _createArtillery.interactable = gold >= _artilleryPrice;
            _createBarrack.interactable = gold >= _barrackPrice;
        }
    }
}