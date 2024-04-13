using SustainTheStrain.Configs.Buildings;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class RocketRadiusVisualizer : BuildingRadiusVisualizer<RocketBuildingConfig>
    {
        [SerializeField] private ZoneVisualizer _sectorVisualizer;

        private IObservable<Vector3> _orientation;

        [Zenject.Inject]
        private void Construct(IObservable<Vector3> orientation)
        {
            _orientation = orientation;
        }

        protected override void Awake()
        {
            base.Awake();
            _orientation.Changed += UpdateDirection;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _orientation.Changed -= UpdateDirection;
        }

        protected override void UpdateConfig(RocketBuildingConfig config)
        {
            base.UpdateConfig(config);
            _sectorVisualizer.Radius = config.Radius;
            _sectorVisualizer.Angle = config.SectorAngle;
        }

        protected override void UpdateSelection(SelectionType selectionType)
        {
            base.UpdateSelection(selectionType);
            gameObject.SetActive(selectionType == SelectionType.Select);
        }

        private void UpdateDirection(Vector3 orientation)
        {
            var lookRotation = Quaternion.LookRotation(orientation - transform.position);
            _sectorVisualizer.Direction = lookRotation.eulerAngles.y;
        }
    }
}