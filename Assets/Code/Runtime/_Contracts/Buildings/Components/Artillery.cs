using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using SustainTheStrain.Abilities;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Artillery : MonoBehaviour, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingViewFactory _buildingViewFactory;

        private ArtilleryMenuView _menuView;

        public ArtilleryData Data { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder, IResourceManager resourceManager,
            IConfigProviderService configProvider, IBuildingViewFactory buildingViewFactory)
        {
            _buildingViewFactory = buildingViewFactory;
            _placeholder = placeholder;
            _resourceManager = resourceManager;

            Data = new ArtilleryData
            (
                startConfig: configProvider.GetBuildingConfig<ArtilleryBuildingConfig>(),
                outline: GetComponent<Outline>()
            );
        }

        public void OnPointerEnter() => Data.Outline.Enable();
        public void OnPointerExit() => Data.Outline.Disable();

        public void OnSelected()
        {
            _menuView.IfNull(() => _menuView = _buildingViewFactory.Create<ArtilleryMenuView, Artillery>(this)).Enable();
            Debug.Log("[ARTILLERY] Show Radius");
        }

        public void OnDeselected()
        {
            _menuView.Disable();
            Debug.Log("[ARTILLERY] Hide Radius");
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