using SustainTheStrain.Buildings.Data;
using SustainTheStrain.Installers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SustainTheStrain.Buildings.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class BuildingCreateButton<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler where T : BuildingData, new()
    {
        private T _data;
        
        private IBuildingSelector _buildingSelector;
        private Button _button;

        [Zenject.Inject]
        private void Construct(IStaticDataService staticDataService) => _data = staticDataService.GetBuilding<T>();

        private void Awake()
        {
            _button = GetComponent<Button>();
            _buildingSelector = GetComponentInParent<IBuildingSelector>();
            _buildingSelector.SelectedData = _data;
            
            if (_buildingSelector == null) Destroy(this);
            else _button.onClick.AddListener(_buildingSelector.CreateBuilding);
        }

        private void OnDestroy() => _button.onClick.RemoveListener(_buildingSelector.CreateBuilding);

        public void OnPointerEnter(PointerEventData eventData) => _buildingSelector.SelectedData = _data;
        public void OnPointerExit(PointerEventData eventData) => _buildingSelector.SelectedData = _data;
    }
}