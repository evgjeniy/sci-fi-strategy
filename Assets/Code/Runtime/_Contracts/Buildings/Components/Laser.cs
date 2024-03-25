using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using SustainTheStrain.Abilities;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Laser : MonoCashed<Outline>, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private System.Func<Laser, string, LaserMenuView> _createMenu;
        private LaserMenuView _menuView;

        public LaserModel Model { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder,
            IResourceManager resourceManager,
            IConfigProviderService configProvider,
            IBuildingViewFactory buildingViewFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _createMenu = buildingViewFactory.Create<LaserMenuView, Laser>;

            Model = new LaserModel(configProvider.GetBuildingConfig<LaserBuildingConfig>());
        }

        public void OnPointerEnter() => Cashed1.Enable();
        public void OnPointerExit() => Cashed1.Disable();
        public void OnSelected() => _menuView.IfNull(() => _menuView = _createMenu(this, null)).Enable();
        public void OnDeselected() => _menuView.Disable();

        public void Upgrade()
        {
            if (_resourceManager.TrySpend(Model.Config.Value.NextLevelPrice) is false) return;
            Model.IncreaseLevel();
        }

        public void Destroy()
        {
            _placeholder.DestroyBuilding();
            _resourceManager.Gold.Value += Model.Config.Value.Compensation;
        }
    }
}