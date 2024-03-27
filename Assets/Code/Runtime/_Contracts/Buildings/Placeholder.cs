using UnityEngine;
using UnityEngine.Extensions;
using Zenject;
using Outline = SustainTheStrain.Abilities.Outline;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IPlaceholder
    {
        public Transform transform { get; }
        public void SetBuilding(IBuilding building);
        public void DestroyBuilding();
    }
    
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(Collider))]
    public class Placeholder : MonoCashed<Outline, Collider>, IPlaceholder, IInputSelectable
    {
        private IBuildingFactory _uiFactory;
        private IInputSystem _inputSystem;

        private BuildingSelectorMenu _selectorMenu;
        private IBuilding _building;

        [Inject]
        private void Construct(IBuildingFactory uiFactory, IInputSystem inputSystem)
        {
            _uiFactory = uiFactory;
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

        public void OnSelected() => _selectorMenu = _uiFactory.CreateSelector(this);
        public void OnDeselected() => _selectorMenu.IfNotNull(x => x.DestroyObject());
    }
}