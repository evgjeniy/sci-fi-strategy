using SustainTheStrain.Buildings.Components;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class BuildingPlaceholder : MonoBehaviour
    {
        [field: SerializeField] public Transform BuildingRoot { get; private set; }

        public Building Building { get; private set; }
        public bool HasBuilding => Building != null;

        public void SetBuilding(Building building)
        {
            Building = building;
            building.SetParent(BuildingRoot);
            building.transform.position = BuildingRoot.position;
        }

        public void DestroyBuilding()
        {
            Destroy(Building.gameObject);
            Building = null;
        }
    }
}
