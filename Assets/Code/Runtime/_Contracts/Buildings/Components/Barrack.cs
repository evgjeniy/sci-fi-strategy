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
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(RecruitGroup))]
    public class Barrack : MonoBehaviour, IBuilding
    {
        [SerializeField] private LayerMask _terrainLayer;

        private IPlaceholder _placeholder;
        private IResourceManager _resourceManager;
        private IBuildingViewFactory _buildingViewFactory;

        private BarrackMenuView _menuView;
        private GameObject _tempPointer;

        public BarrackData Data { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder, IResourceManager resourceManager,
            IConfigProviderService configProvider, IBuildingViewFactory buildingViewFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _buildingViewFactory = buildingViewFactory;

            Data = new BarrackData
            (
                startConfig: configProvider.GetBuildingConfig<BarrackBuildingConfig>(),
                outline: GetComponent<Outline>(),
                recruitGroup: GetComponent<RecruitGroup>()
            );
        }

        public void OnPointerEnter() => Data.Outline.Enable();
        public void OnPointerExit() => Data.Outline.Disable();

        public void OnSelected()
        {
            _menuView.IfNull(() => _menuView = _buildingViewFactory.Create<BarrackMenuView, Barrack>(this)).Enable();
            Debug.Log("[BARRACK] Show Radius");
        }

        public void OnDeselected()
        {
            _menuView.Disable();
            Debug.Log("[BARRACK] Hide Radius");
        }

        public IInputState OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (Data.IsUnitsPointState is false)
                return currentState;

            Data.RecruitGroup.GuardPost.Position = _tempPointer.transform.position;
            Data.IsUnitsPointState = false;

            Destroy(_tempPointer);

            return new IdleState();
        }

        public IInputState OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (Data.IsUnitsPointState is false)
                return currentState;

            if (Physics.Raycast(ray, out var hit, float.MaxValue, _terrainLayer) is false)
                return currentState;

            var barrackPosition = transform.position;
            var directionToPoint = hit.point - barrackPosition;
            var distance = Mathf.Min(directionToPoint.magnitude, Data.Config.Value.Radius);

            _tempPointer.transform.position = barrackPosition + directionToPoint.normalized * distance;
            return currentState;
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

        public void UnitsPointStateToggle()
        {
            Data.IsUnitsPointState = true;

            _menuView.Disable();
            _tempPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere)
                .With(x => x.GetComponent<MeshRenderer>().material.color = Color.red)
                .With(x => x.SetParent(transform));
        }
    }
}