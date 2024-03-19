using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BuildingCreateMenu : MonoBehaviour
    {
        [SerializeField] private Button _createRocket;
        [SerializeField] private Button _createLaser;
        [SerializeField] private Button _createArtillery;
        [SerializeField] private Button _createBarrack;
        
        private IPlaceholder _placeholder;

        public IPlaceholder Placeholder
        {
            get => _placeholder;
            set
            {
                if (value == null) return;
                _placeholder = value;
                
                var worldCamera = Camera.main;
                if (worldCamera == null) return;

                var cameraTransform = worldCamera.transform;
                var placeholderPosition = _placeholder.transform.position;
                
                transform.position = Vector3.Lerp(placeholderPosition, cameraTransform.position, 0.15f);
                transform.LookAt(placeholderPosition - cameraTransform.position, cameraTransform.up);
            }
        }

        // [Inject]
        // private void Construct(IFactory<BuildingType, Building> buildingFactory)
        // {
        //     _createRocket.onClick.AddListener(() => Placeholder.SetBuilding(buildingFactory.Create(BuildingType.Rocket)));
        //     _createLaser.onClick.AddListener(() => Placeholder.SetBuilding(buildingFactory.Create(BuildingType.Laser)));
        //     _createArtillery.onClick.AddListener(() => Placeholder.SetBuilding(buildingFactory.Create(BuildingType.Artillery)));
        //     _createBarrack.onClick.AddListener(() => Placeholder.SetBuilding(buildingFactory.Create(BuildingType.Barrack)));
        // }

        public class Factory : PlaceholderFactory<IPlaceholder, BuildingCreateMenu> {}
    }
}