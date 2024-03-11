using System;
using SustainTheStrain.Buildings.Components;
using SustainTheStrain.Roads;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Buildings
{
    public class BuildingPlaceholder : MonoBehaviour
    {
        [SerializeField] private Road _road;
        [field: SerializeField] public Transform BuildingRoot { get; private set; }
        public Building Building { get; private set; }
        public bool HasBuilding => Building != null;

        private void OnEnable()
        {
            _road.IfNull(_ =>
            {
                Debug.LogWarning($"[{gameObject.name}] Road not assigned");
            });
        }

        public void SetBuilding(Building building)
        {
            Building = building;
            building.SetParent(BuildingRoot);
            building.transform.position = BuildingRoot.position;
            building.transform.localScale = Vector3.one; // TEMP

            _road.IfNotNull(_ =>
            {
                building.Orientation = _road.Project(BuildingRoot.position);
            });
        }

        public void DestroyBuilding()
        {
            Destroy(Building.gameObject);
            Building = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position,  (_road.Project(BuildingRoot.position) - transform.position).normalized * 3);      
        }
    }
}
