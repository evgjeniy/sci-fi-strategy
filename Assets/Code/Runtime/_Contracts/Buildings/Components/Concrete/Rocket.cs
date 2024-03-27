using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using SustainTheStrain.Abilities;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Rocket : MonoBehaviour, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingFactoryUI _uiFactory;

        private RocketManagementMenu _managementMenu;

        public RocketData Data { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder, IResourceManager resourceManager,
            IConfigProviderService configProvider, IBuildingFactoryUI uiFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _uiFactory = uiFactory;

            Data = new RocketData
            (
                startConfig: configProvider.GetBuildingConfig<RocketBuildingConfig>(),
                outline: GetComponent<Outline>()
            );
        }

        public void OnPointerEnter() => Data.Outline.Enable();
        public void OnPointerExit() => Data.Outline.Disable();

        public void OnSelected()
        {
            if (_managementMenu == null)
                _managementMenu = _uiFactory.Create<RocketManagementMenu>(this);
            
            _managementMenu.Enable();
            Debug.Log("[ROCKET] Show Radius");
        }

        public void OnDeselected()
        {
            _managementMenu.Disable();
            Debug.Log("[ROCKET] Hide Radius");
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