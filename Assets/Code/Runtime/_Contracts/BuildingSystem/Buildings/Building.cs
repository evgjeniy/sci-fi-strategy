using UnityEngine;
using Zenject;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public abstract class Building : MonoBehaviour
    {
        protected int CurrentLevel = 0;
        
        public class Factory : PlaceholderFactory<BuildingType, IPlaceholder, Building> {}
    }
}