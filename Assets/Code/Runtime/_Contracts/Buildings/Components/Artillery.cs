using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using SustainTheStrain.Abilities;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Artillery : MonoCashed<Outline>, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingViewFactory _buildingViewFactory;
        private ArtilleryMenuView _menuView;

        public ArtilleryModel Model { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder,
            IResourceManager resourceManager,
            IConfigProviderService configProvider,
            IBuildingViewFactory buildingViewFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _buildingViewFactory = buildingViewFactory;

            Model = new ArtilleryModel(configProvider.GetBuildingConfig<ArtilleryBuildingConfig>(1));
        }

        public void OnPointerEnter() => Cashed1.Enable();
        public void OnPointerExit() => Cashed1.Disable();

        public void OnSelected()
        {
            _menuView = _buildingViewFactory.Create<ArtilleryMenuView, Artillery>(this)
                .With(x => x.SetParent(transform))
                .With(x => x.transform.LookAtCamera(transform));
        }

        public void OnDeselected()
        {
            _menuView.IfNotNull(x => x.DestroyObject());
        }

        public void Upgrade()
        {
            if (_resourceManager.TrySpend(Model.Config.NextLevelPrice) is false) return;
            Model.IncreaseLevel();
        }

        public void Destroy()
        {
            _placeholder.DestroyBuilding();
            _resourceManager.Gold.Value += Model.Config.Compensation;
        }
    }
}