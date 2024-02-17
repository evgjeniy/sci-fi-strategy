using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Buildings.UI.Menus;
using SustainTheStrain.Installers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SustainTheStrain.Buildings.UI.Buttons.Creation
{
    [RequireComponent(typeof(Button))]
    public abstract class BuildingCreateButton<T> : BaseBuildingButton<IBuildingCreateMenu>,
        IPointerEnterHandler, IPointerExitHandler where T : BuildingData, new()
    {
        private T _data;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService) => _data = staticDataService.GetBuilding<T>();

        protected override UnityAction ButtonAction => Menu.CreateBuilding;

        public void OnPointerEnter(PointerEventData eventData) => Menu.SelectedData = _data;
        public void OnPointerExit(PointerEventData eventData) => Menu.SelectedData = _data;
    }
}