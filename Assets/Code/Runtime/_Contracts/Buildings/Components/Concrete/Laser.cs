using System;
using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using SustainTheStrain.Abilities;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Laser : MonoBehaviour, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingFactoryUI _uiFactory;

        private LaserManagementMenu _managementMenu;

        public LaserData Data { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder, IResourceManager resourceManager,
            IConfigProviderService configProvider, IBuildingFactoryUI uiFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _uiFactory = uiFactory;

            Data = new LaserData
            (
                startConfig: configProvider.GetBuildingConfig<LaserBuildingConfig>(),
                outline: GetComponent<Outline>()
            );
        }

        public void OnPointerEnter() => Data.Outline.Enable();
        public void OnPointerExit() => Data.Outline.Disable();

        public void OnSelected()
        {
            if (_managementMenu == null)
                _managementMenu = _uiFactory.Create<LaserManagementMenu>(this);
            
            _managementMenu.Enable();
            Debug.Log("[LASER] Show Radius");
        }

        public void OnDeselected()
        {
            _managementMenu.Disable();
            Debug.Log("[LASER] Hide Radius");
        }

        public void Upgrade()
        {
            if (_resourceManager.TrySpend(Data.Config.Value.NextLevelPrice) is false) return;
            if (Data.Config.Value.NextLevelConfig == null) return;

            Data.Config.Value = Data.Config.Value.NextLevelConfig;
        }

        public void Destroy()
        {
            _placeholder.DestroyBuilding();
            _resourceManager.Gold.Value += Data.Config.Value.Compensation;
        }
    }
}