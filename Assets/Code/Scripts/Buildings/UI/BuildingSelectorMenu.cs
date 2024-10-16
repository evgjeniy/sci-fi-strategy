﻿using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.ResourceSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain.Buildings
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
            _createRocket.onClick.AddListener(() => CreateBuilding(buildingFactory.CreateRocket, placeholder, _rocketPrice));
            _createLaser.onClick.AddListener(() => CreateBuilding(buildingFactory.CreateLaser, placeholder, _laserPrice));
            _createArtillery.onClick.AddListener(() => CreateBuilding(buildingFactory.CreateArtillery, placeholder, _artilleryPrice));
            _createBarrack.onClick.AddListener(() => CreateBuilding(buildingFactory.CreateBarrack, placeholder, _barrackPrice));
        }

        private void CreateBuilding(System.Func<IPlaceholder, IBuilding> createBuilding, IPlaceholder placeholder, int price)
        {
            if (!_resourceManager.TrySpend(price)) return;
            
            var building = createBuilding(placeholder);
            placeholder.SetBuilding(building);
        }

        private void DisplayButtonInteraction(int currentGold)
        {
            _createRocket.interactable = currentGold >= _rocketPrice;
            _createLaser.interactable = currentGold >= _laserPrice;
            _createArtillery.interactable = currentGold >= _artilleryPrice;
            _createBarrack.interactable = currentGold >= _barrackPrice;
        }
    }
}