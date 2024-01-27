using UnityEngine;

namespace SustainTheStrain.Buildings.Components
{
    public abstract class Building : MonoBehaviour
    {
        public int CurrentUpgradeLevel { get; protected set; }
    }
}