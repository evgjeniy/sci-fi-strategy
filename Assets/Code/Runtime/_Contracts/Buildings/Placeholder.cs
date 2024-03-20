using UnityEngine;
using UnityEngine.Extensions;
using Zenject;
using Outline = SustainTheStrain.Abilities.Outline;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IPlaceholder
    {
        public Transform transform { get; }
        public void SetBuilding(Building building);
        public void DestroyBuilding();
    }
    
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(Collider))]
    public class Placeholder : MonoCashed<Outline, Collider>, IPlaceholder, IInputSelectable
    {
        private IBuildingCreateMenuFactory _createMenuFactory;
        private IInputSystem _inputSystem;

        private BuildingCreateMenu _createMenu;
        private Building _building;

        [Inject]
        private void Construct(IBuildingCreateMenuFactory createMenuFactory, IInputSystem inputSystem)
        {
            _createMenuFactory = createMenuFactory;
            _inputSystem = inputSystem;
        }

        public void SetBuilding(Building building)
        {
            Cashed2.Disable();
            
            _building = building;
            _building.transform.parent = transform;
            _building.transform.localPosition = Vector3.zero;

            _createMenu.IfNotNull(x => x.DestroyObject());
            _inputSystem.Select(building);
        }

        public void DestroyBuilding()
        {
            _building.IfNotNull(x => x.DestroyObject());
            _inputSystem.Select(this);

            Cashed2.Enable();
        }

        public void OnPointerEnter() => Cashed1.Enable();
        public void OnPointerExit() => Cashed1.Disable();

        public void OnSelected() => _createMenu = _createMenuFactory.Create(this);
        public void OnDeselected() => _createMenu.IfNotNull(x => x.DestroyObject());

        public void OnSelectedRightClick(Ray ray) => _inputSystem.Idle();
        public void OnSelectedLeftClick(Ray ray) {}
        public void OnSelectedUpdate(Ray ray) {}
    }
}