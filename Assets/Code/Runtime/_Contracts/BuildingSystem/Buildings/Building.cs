using UnityEngine;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public abstract class Building : MonoBehaviour, IInputSelectable
    {
        protected int CurrentLevel = 0;
    }
}