using SustainTheStrain.Abilities;
using SustainTheStrain.Input;
using SustainTheStrain.Roads;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(Collider))]
    public class Placeholder : MonoCashed<Outline, Collider>, IPlaceholder, IInputSelectable
    {
        [field: SerializeField] public Road Road { get; private set; }

        private IBuildingFactory _buildingFactory;
        private IInputSystem _inputSystem;

        private BuildingSelectorMenu _selectorMenu;
        private IBuilding _building;

        [Inject]
        private void Construct(IBuildingFactory buildingFactory, IInputSystem inputSystem)
        {
            _buildingFactory = buildingFactory;
            _inputSystem = inputSystem;
        }

        public void SetBuilding(IBuilding building)
        {
            Cashed2.Disable();
            
            _building = building;
            _building.transform.parent = transform;
            _building.transform.localPosition = Vector3.zero;

            _selectorMenu.IfNotNull(x => x.DestroyObject());
            _inputSystem.Select(building);
        }

        public void DestroyBuilding()
        {
            _building.IfNotNull(x => x.transform.DestroyObject());
            _inputSystem.Select(this);

            Cashed2.Enable();
        }

        public void OnPointerEnter() => Cashed1.Enable();
        public void OnPointerExit() => Cashed1.Disable();

        public void OnSelected() => _selectorMenu = _buildingFactory.CreateSelector(this);
        public void OnDeselected() => _selectorMenu.IfNotNull(x => x.DestroyObject());
    }
}