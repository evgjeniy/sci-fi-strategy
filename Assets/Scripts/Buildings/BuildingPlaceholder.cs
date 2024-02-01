using SustainTheStrain.Buildings.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class BuildingPlaceholder : MonoBehaviour
    {
        [field: SerializeField] public Transform BuildingRoot { get; private set; }
        [field: SerializeField] public Transform SelectorUIRoot { get; private set; }
        
        private Building _building;

        // [Zenject.Inject] private BuildingSystem _system;
    }
}
