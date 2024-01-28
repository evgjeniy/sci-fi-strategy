using UnityEngine;
using Zenject;

namespace SustainTheStrain.Buildings.Components
{
    public abstract class Building : MonoBehaviour
    {
        public int CurrentUpgradeLevel { get; protected set; }

        public class Factory : PlaceholderFactory<Building> {}
    }
}