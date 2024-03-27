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
        private IBuildingFactoryUI _uiFactory;

        private BarrackManagementMenu _managementMenu;

        public BarrackData Data { get; private set; }

        [Inject]
        private void Construct(IPlaceholder placeholder, IResourceManager resourceManager,
            IConfigProviderService configProvider, IBuildingFactoryUI uiFactory)
        {
            _placeholder = placeholder;
            _resourceManager = resourceManager;
            _uiFactory = uiFactory;

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
            if (_managementMenu == null)
                _managementMenu = _uiFactory.Create<BarrackManagementMenu>(this);
            
            _managementMenu.Enable();
            Debug.Log("[BARRACK] Show Radius");
        }

        public void OnDeselected()
        {
            _managementMenu.Disable();
            Debug.Log("[BARRACK] Hide Radius");

            Data.RecruitsPointer.IfNotNull(x => x.DestroyObject());
        }

        public IInputState OnSelectedLeftClick(IInputState currentState, Ray ray)
        {
            if (Data.RecruitsPointer == null)
                return currentState;

            Data.RecruitGroup.GuardPost.Position = Data.RecruitsPointer.transform.position;
            return new IdleState();
        }

        public IInputState OnSelectedUpdate(IInputState currentState, Ray ray)
        {
            if (Data.RecruitsPointer == null)
                return currentState;

            if (Physics.Raycast(ray, out var hit, float.MaxValue, _terrainLayer) is false)
                return currentState;

            var barrackPosition = transform.position;
            var directionToPoint = hit.point - barrackPosition;
            var distance = Mathf.Min(directionToPoint.magnitude, Data.Config.Value.Radius);

            Data.RecruitsPointer.transform.position = barrackPosition + directionToPoint.normalized * distance;
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

        public void SetUnitsPointState()
        {
            Data.RecruitsPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere)
                .With(x => x.GetComponent<MeshRenderer>().material.color = Color.red)
                .With(x => x.SetParent(transform));;

            _managementMenu.Disable();
        }
    }
}