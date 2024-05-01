using SustainTheStrain.Buildings;
using UnityEngine;

namespace SustainTheStrain
{
    public class BuildingAreaDataProvider : IAreaDataProvider
    {
        private readonly IBuilding _building;
        public BuildingAreaDataProvider(IBuilding building) => _building = building;

        public Vector3 Center => _building.transform.position;
        public float Radius => _building.Config.Radius;
        public LayerMask Mask => _building.Config.Mask;
    }
}