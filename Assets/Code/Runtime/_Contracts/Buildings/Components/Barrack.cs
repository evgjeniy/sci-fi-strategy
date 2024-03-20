using SustainTheStrain._Contracts.Configs;
using SustainTheStrain._Contracts.Installers;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class Barrack : Building
    {
        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingViewFactory _buildingViewFactory;
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
            _buildingViewFactory = buildingViewFactory;

            Model = new BarrackModel(this, configProvider);
        }

        public override void OnSelected()
        {
            _menuView = _buildingViewFactory.Create<BarrackMenuView, BarrackModel>(Model)
                .With(x => x.SetParent(transform))
                .With(x => x.transform.LookAtCamera(transform));
        }

        public override void OnDeselected()
        {
            _menuView.IfNotNull(x => x.DestroyObject());
        }

        public override void Upgrade()
        {
            if (_resourceManager.TrySpend(Model.NextLevelPrice) is false) return;
            Model.IncreaseLevel();
        }

        public override void Destroy()
        {
            _placeholder.DestroyBuilding();
            _resourceManager.Gold.Value += Model.Compensation;
        }
    }
}