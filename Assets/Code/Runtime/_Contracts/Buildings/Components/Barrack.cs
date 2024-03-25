using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain._Contracts.Installers;
using SustainTheStrain.Abilities;
using SustainTheStrain.Units;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Barrack : MonoCashed<Outline, RecruitGroup>, IBuilding
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private System.Func<Barrack, string, BarrackMenuView> _createMenu;
        private BarrackMenuView _menuView;

        public BarrackModel Model { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder,
            IResourceManager resourceManager,
            IConfigProviderService configProvider,
            IBuildingViewFactory buildingViewFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _createMenu = buildingViewFactory.Create<BarrackMenuView, Barrack>;

            Model = new BarrackModel(configProvider.GetBuildingConfig<BarrackBuildingConfig>());
        }

        public void OnPointerEnter() => Cashed1.Enable();
        public void OnPointerExit() => Cashed1.Disable();
        public void OnSelected() => _menuView.IfNull(() => _menuView = _createMenu(this, null)).Enable();
        public void OnDeselected() => _menuView.Disable();

        public void OnSelectedLeftClick(Ray ray)
        {
            if (!Model.IsUnitsPointState.Value) return;
            if (!Physics.Raycast(ray, out var hit)) return;

            Cashed2.GuardPost.Position = hit.point;
        }

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

        public void UnitsPointStateToggle()
        {
            Model.ToggleUnitsPointState();
        }
    }
}